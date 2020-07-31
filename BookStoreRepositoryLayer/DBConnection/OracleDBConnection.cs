using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BookStoreRepositoryLayer.DBConnection
{
    public class OracleDBConnection
    {
        private readonly IConfiguration configuration;
        public OracleDBConnection()
        {
            
        }
        public OracleDBConnection(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void Connection()
        {
            Console.WriteLine("Starting.\r\n");
            using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
            {
                Console.WriteLine("Open connection...");
                _db.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = _db;
                cmd.CommandText =
                "begin " +
                "execute immediate 'create table BOOKSTORE.Registration(UserId int NOT NULL PRIMARY KEY ,FirstName varchar2(20) not null,LastName varchar2(20) not null ,Email varchar2(20) not null,Password  varchar2(20) not null )';" +
                "execute immediate 'create table BOOKSTORE.BookDetail(BookId int NOT NULL PRIMARY KEY, BookName varchar2(20) not null,AuthorName varchar2(20) not null,Price float,Quantity int not null,Catagory varchar2(20) not null ,Rating float,AddToCart varchar2(20) not null,AddToWishlist varchar2(20))';" +
                "end;";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                _db.Close();
            }
        }
    }
}
