using BookStoreCommonLayer;
using BookStoreCommonLayer.BookDetail;
using BookStoreCommonLayer.BookModel;
using BookStoreCommonLayer.Queries;
using BookStoreRepositoryLayer.IRepository;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BookStoreRepositoryLayer.RepositoryImplemantation
{
    public class OrderDetialsReposiory: IOrderDetialsRepository
    {
       private readonly IConfiguration configuration;
        public OrderDetialsReposiory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public object PlaceOrder(OrderDetails orderDetail)
        {
            try
            {
                var commandText = Queries.PlaceOrderQuery;
                using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                using (OracleCommand cmd = new OracleCommand(commandText, _db))
                {
                    cmd.Connection = _db;
                    cmd.Parameters.Add("OrderId", orderDetail.OrderId);
                    cmd.Parameters.Add("Address", orderDetail.Address);
                    cmd.Parameters.Add("City", orderDetail.City);
                    cmd.Parameters.Add("PinCode", orderDetail.PinCode);
                    cmd.Parameters.Add("Phone_no", orderDetail.Phone_no);
                    cmd.Parameters.Add("PaymentMethod", orderDetail.PaymentMethod);
                    cmd.Parameters.Add("Quantity", orderDetail.Quantity);
                    cmd.Parameters.Add("UUserId ", orderDetail.UUserId);
                    cmd.Parameters.Add("BBookId ", orderDetail.BBookId);
                    _db.Open();
                    cmd.ExecuteNonQuery();
                    _db.Close();
                }
                BooksDetails book = new BooksDetails();
                var commandTexts = Queries.GetBookDetailByBookIdQuery + "'" + orderDetail.BBookId + "'";
                using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                using (OracleCommand cmd1 = new OracleCommand(commandTexts, _db))
                {
                    cmd1.Connection = _db;
                    cmd1.CommandType = CommandType.Text;
                    _db.Open();
                    OracleDataReader reader = cmd1.ExecuteReader();
                    while (reader.Read())
                    {                      
                        book.Price = Convert.ToDouble(reader["Price"]);
                        book.Quantity = Convert.ToInt32(reader["Quantity"]);                                    
                    }
                    _db.Close();
                }
                int quan = book.Quantity - orderDetail.Quantity;
                var commandTextss = Queries.bookstoreUpdateQuery + "'" + orderDetail.BBookId + "'"; ;
                using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                using (OracleCommand cmd2 = new OracleCommand(commandTextss, _db))
                {
                    cmd2.Connection = _db;
                    _db.Open();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Parameters.Add("Quantity", quan);
                    cmd2.ExecuteNonQuery();
                    _db.Close();
                }
                if (book.Quantity == 0)
                {
                    return "OutOfStack";
                }
                return " Total Price = "+ Math.Round(Convert.ToDouble(orderDetail.Quantity) * book.Price);
            }           
            catch (CustomException exception)
            {
                throw new CustomException(CustomException.ExceptionType.INVALID_INPUT, exception.Message);
            }
        }
        public IEnumerable<OrderDetails> ViewOrderPlaced()
        {
            try
            {
                List<OrderDetails> list = new List<OrderDetails>();
                var commandText = Queries.GetOrderDetailQuery;
                using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                using (OracleCommand cmd = new OracleCommand(commandText, _db))
                {
                    cmd.Connection = _db;
                    cmd.CommandType = CommandType.Text;
                    _db.Open();
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        OrderDetails Order = new OrderDetails();
                        Order.OrderId = Convert.ToInt32(reader["OrderId"]);
                        Order.Address = reader["Address"].ToString();
                        Order.City = reader["City"].ToString();
                        Order.PinCode = Convert.ToInt32(reader["PinCode"]);
                        Order.Phone_no = Convert.ToInt32(reader["Phone_no"]);
                        Order.PaymentMethod = reader["PaymentMethod"].ToString();
                        Order.Quantity = Convert.ToInt32(reader["Quantity"]);
                        Order.UUserId = Convert.ToInt32(reader["UUserId"]);
                        Order.BBookId = Convert.ToInt32(reader["BBookId"]);
                        list.Add(Order);
                    }
                    _db.Close();
                    return list;
                }
            }
            catch (CustomException exception)
            {
                throw new CustomException(CustomException.ExceptionType.INVALID_INPUT, exception.Message);
            }
        }
        
        public String EmailOrderNumber(int UserId)
        {
            string Email = string.Empty;          
            var commandTexts = Queries.GetemailQuery + "'" + UserId + "'";
            using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
            using (OracleCommand cmd1 = new OracleCommand(commandTexts, _db))
            {
                cmd1.Connection = _db;
                cmd1.CommandType = CommandType.Text;
                _db.Open();
                OracleDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {                
                    Email = reader["Email"].ToString();
                }
                _db.Close();
            }
            return Email;            
        }

        public bool OrderConfirmationToEmail(int UserId, int orderid)
        {
            string Email = EmailOrderNumber(UserId);
            string orderdetails = "Dear customer, we have request your order and it will be delivered to u soon keep shopping " +
                orderid + " is your order Number, keep it for tracking order.";
            //// for sending message in MSMQ
            MessageQueue msgqueue;
            if (MessageQueue.Exists(@".\Private$\MyQueue"))
            {
                msgqueue = new MessageQueue(@".\Private$\MyQueue");
            }
            else
            {
                msgqueue = MessageQueue.Create(@".\Private$\MyQueue");
            }
            Message message = new Message();
            message.Formatter = new BinaryMessageFormatter();
            message.Body = orderdetails;
            msgqueue.Label = "order number";
            msgqueue.Send(message);
            //// for reading message from MSMQ
            var receivequeue = new MessageQueue(@".\Private$\MyQueue");
            var receivemsg = receivequeue.Receive();
            receivemsg.Formatter = new BinaryMessageFormatter();
            string linktobesend = receivemsg.Body.ToString();
            if (Sendmail(Email, linktobesend))
            {
                return true;
            }
            return false;
        }
        private bool Sendmail(string email, string message)
        {

            MailMessage mailmessage = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            mailmessage.From = new MailAddress("zzone3369@gmail.com");
            mailmessage.To.Add(new MailAddress(email));
            mailmessage.Subject = "Order Confirmation";
            mailmessage.IsBodyHtml = true;
            mailmessage.Body = message;
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new NetworkCredential("zzone3369@gmail.com", "123qwe@@");
            smtp.EnableSsl = true;
            smtp.Send(mailmessage);
            return true;
        }
    }
}


