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
    public class BookDetialsRepository : IBookDetialsRepository
    {
        private readonly IConfiguration configuration;
        public BookDetialsRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public object AddBookDetails(BooksDetails booksDetail)
        {
            try
            {
                var commandText = Queries.BookDitailsQuery;
                using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                using (OracleCommand cmd = new OracleCommand(commandText, _db))
                {
                    cmd.Connection = _db;
                    cmd.Parameters.Add("BookId", booksDetail.BookId);
                    cmd.Parameters.Add("BookName", booksDetail.BookName);
                    cmd.Parameters.Add("AuthorName", booksDetail.AuthorName);
                    cmd.Parameters.Add("Price", booksDetail.Price);
                    cmd.Parameters.Add("Quantity", booksDetail.Quantity);
                    cmd.Parameters.Add("Catagory", booksDetail.Catagory);
                    cmd.Parameters.Add("Rating", booksDetail.Rating);
                    cmd.Parameters.Add("AddToCart", booksDetail.AddToCart);
                    cmd.Parameters.Add("AddToWishlist", booksDetail.AddToWishlist);
                    _db.Open();
                    cmd.ExecuteNonQuery();
                    _db.Close();
                    return booksDetail.BookId + " = BookID is sucessfull added";
                }
            }
            catch (CustomException exception)
            {
                throw new CustomException(CustomException.ExceptionType.INVALID_INPUT, exception.Message);
            }
        }
        public IEnumerable<BooksDetails> GetBookDetails()
        {
            try
            {
                List<BooksDetails> list = new List<BooksDetails>();
                var commandText = Queries.GetBookDetailQuery;
                using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                using (OracleCommand cmd = new OracleCommand(commandText, _db))
                {
                    cmd.Connection = _db;
                    cmd.CommandType = CommandType.Text;
                    _db.Open();
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        BooksDetails booksDetail = new BooksDetails();
                        booksDetail.BookId = Convert.ToInt32(reader["BookId"]);
                        booksDetail.BookName = reader["BookName"].ToString();
                        booksDetail.AuthorName = reader["AuthorName"].ToString();
                        booksDetail.Price = Convert.ToDouble(reader["Price"]);
                        booksDetail.Quantity = Convert.ToInt32(reader["Quantity"]);
                        booksDetail.Catagory = reader["Catagory"].ToString();
                        booksDetail.Rating = Convert.ToDouble(reader["Rating"]);
                        booksDetail.AddToCart = reader["AddToCart"].ToString();
                        booksDetail.AddToWishlist= reader["AddToWishlist"].ToString();
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

        public IEnumerable<BooksDetails> GetBookDetailsByBookName(string bookName)
        {
            try
            {
                List<BooksDetails> list = new List<BooksDetails>();
                bookName = bookName.Replace('"', ' ').Trim();
                var commandText = Queries.GetBookDetailByBookNameQuery + "'" + bookName + "'"; 
                using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                using (OracleCommand cmd = new OracleCommand(commandText, _db))
                {
                    cmd.Connection = _db;
                    cmd.CommandType = CommandType.Text;
                    _db.Open();
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        BooksDetails book = new BooksDetails();
                        book.BookId = Convert.ToInt32(reader["BookId"]);
                        book.BookName = reader["BookName"].ToString();
                        book.AuthorName = reader["AuthorName"].ToString();
                        book.Price = Convert.ToDouble(reader["Price"]);
                        book.Quantity = Convert.ToInt32(reader["Quantity"]);
                        book.Catagory = reader["Catagory"].ToString();
                        book.Rating = Convert.ToDouble(reader["Rating"]);
                        book.AddToCart = reader["AddToCart"].ToString();
                        book.AddToWishlist = reader["AddToWishlist"].ToString();
                        list.Add(book);
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

        public IEnumerable<BooksDetails> GetBookDetailsByBookId(int bookid)
        {
            try
            {
                List<BooksDetails> list = new List<BooksDetails>();
                var commandText = Queries.GetBookDetailByBookIdQuery + "'" + bookid + "'";
                using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                using (OracleCommand cmd = new OracleCommand(commandText, _db))
                {
                    cmd.Connection = _db;
                    cmd.CommandType = CommandType.Text;
                    _db.Open();
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        BooksDetails book = new BooksDetails();
                        book.BookId = Convert.ToInt32(reader["BookId"]);
                        book.BookName = reader["BookName"].ToString();
                        book.AuthorName = reader["AuthorName"].ToString();
                        book.Price = Convert.ToDouble(reader["Price"]);
                        book.Quantity = Convert.ToInt32(reader["Quantity"]);
                        book.Catagory = reader["Catagory"].ToString();
                        book.Rating = Convert.ToDouble(reader["Rating"]);
                        book.AddToCart = reader["AddToCart"].ToString();
                        book.AddToWishlist = reader["AddToWishlist"].ToString();
                        list.Add(book);
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
