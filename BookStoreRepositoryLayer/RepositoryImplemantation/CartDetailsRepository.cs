using BookStoreCommonLayer;
using BookStoreCommonLayer.BookDetail;
using BookStoreCommonLayer.Queries;
using BookStoreRepositoryLayer.IRepository;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BookStoreRepositoryLayer.RepositoryImplemantation
{
    public class CartDetailsRepository : ICartDetialsRepository
    {
        private readonly IConfiguration configuration;
        public CartDetailsRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public object AddCartDetails(int bookid)
        {
            try
            {
                var commandText = Queries.AddToCartQuery + bookid + ""; 
                using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                using (OracleCommand cmd = new OracleCommand(commandText, _db))
                {
                    cmd.Connection = _db;
                    _db.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("AddToCart", "True");                    
                    cmd.ExecuteNonQuery();
                    _db.Close();
                }
                return bookid + " = BookID is sucessfull added to Cart";
            }
            catch (CustomException exception)
            {
                throw new CustomException(CustomException.ExceptionType.INVALID_INPUT, exception.Message);
            }
        }

    public IEnumerable<BooksDetails> GetCartItems()
        {
            try
            {
                List<BooksDetails> list = new List<BooksDetails>();
                BooksDetails booksDetail = new BooksDetails();
                var commandText = Queries.GetCartDetailQuery;
                using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                using (OracleCommand cmd = new OracleCommand(commandText, _db))
                {
                    cmd.Connection = _db;
                    cmd.CommandType = CommandType.Text;
                    _db.Open();
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        booksDetail.BookId = Convert.ToInt32(reader["BookId"]);
                        booksDetail.BookName = reader["BookName"].ToString();
                        booksDetail.AuthorName = reader["AuthorName"].ToString();
                        booksDetail.Price = Convert.ToDouble(reader["Price"]);
                        booksDetail.Quantity = Convert.ToInt32(reader["Quantity"]);
                        booksDetail.Catagory = reader["Catagory"].ToString();
                        booksDetail.Rating = Convert.ToDouble(reader["Rating"]);
                        booksDetail.AddToCart = reader["AddToCart"].ToString();
                        booksDetail.AddToWishlist = reader["AddToWishlist"].ToString();
                        list.Add(booksDetail);
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
    }
}
