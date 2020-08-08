using BookStoreCommonLayer.BookDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManagerLayer.IManager
{
    public interface IBookStoreManager
    {
        object AddBookDetails(BooksDetails booksDetail);
        object GetBookDetails();
        object GetBookDetailsByBookName(string bookName);
        object GetBookDetailsByBookId(int bookid);
    }
}
