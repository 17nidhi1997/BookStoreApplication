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
        public static string PlaceOrderQuery = "insert into BOOKSTORE.OrderDetails(OrderId ,Address ,City ,PinCode ,Phone_no  ,PaymentMethod ,Quantity ,UUserId ,BBookId) values(:OrderId,:Address,:City,:PinCode,:Phone_no,:PaymentMethod,:Quantity,:UUserId,:BBookId)";
        public static string GetBookDetailQuery = "Select * from BOOKSTORE.BookDetail";
        public static string GetOrderDetailQuery = "Select * from BOOKSTORE.OrderDetails";
        public static string GetemailQuery = "Select Email from BOOKSTORE.Registration where UserId=";
        public static string GetBookDetailByBookNameQuery = "Select * from BOOKSTORE.BookDetail where BookName=";
        public static string GetBookDetailByBookIdQuery = "Select * from BOOKSTORE.BookDetail where BookId=";
        public static string AddToCartQuery = "UPDATE BOOKSTORE.BookDetail SET AddToCart = 'TRUE' WHERE BookId=";
        public static string AddToWishListQuery = "UPDATE BOOKSTORE.BookDetail SET AddToWishlist = 'TRUE' WHERE BookId=";
        public static string GetCartDetailQuery = "Select * from BOOKSTORE.BookDetail WHERE AddToCart = 'TRUE'";
        public static string GetWishListDetailQuery = "Select * from BOOKSTORE.BookDetail WHERE AddToWishlist = 'TRUE'";
        public static string bookstoreUpdateQuery = "UPDATE BOOKSTORE.BookDetail SET Quantity= :quan WHERE BookId=";
    }
}
