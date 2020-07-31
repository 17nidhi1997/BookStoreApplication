using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreCommonLayer.Queries
{
    public class Queries
    {
        public static string userDitailsQuery = "insert into BOOKSTORE.Registration(UserId ,FirstName ,LastName ,Email ,Password) values(:UserId,:FirstName,:LastName,:Email,:Password)";
        public static string loginRolesQuery = "Select * from BOOKSTORE.Registration where  Email= :email and Password= :password";
        public static string BookDitailsQuery = "insert into BOOKSTORE.BookDetail(BookId ,BookName ,AuthorName ,Price ,Quantity ,Catagory ,Rating ,AddToCart ,AddToWishlist) values(:BookId,:BookName,:AuthorName,:Price,:Quantity,:Catagory,:Rating,:AddToCart,:AddToWishlist)";
        public static string GetBookDetailQuery = "Select * from BOOKSTORE.BookDetail";
        public static string GetBookDetailByBookNameQuery = "Select * from BOOKSTORE.BookDetail where BookName=";
        public static string AddToCartQuery = "UPDATE BOOKSTORE.BookDetail SET AddToCart = 'TRUE' WHERE BookId=";
        public static string GetCartDetailQuery = "Select * from BOOKSTORE.BookDetail WHERE AddToCart = 'TRUE'";
    }
}
