using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreCommonLayer.BookDetail
{
    public class BooksDetails
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Catagory { get; set; }
        public double Rating { get; set; }
        public string AddToCart { get; set; }
        public string AddToWishlist { get; set; }
    }
}
