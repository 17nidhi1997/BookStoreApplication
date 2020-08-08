using BookStoreCommonLayer.BookDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepositoryLayer.IRepository
{
    public interface IBookDetialsRepository
    {
        object AddBookDetails(BooksDetails booksDetail);
        IEnumerable<BooksDetails> GetBookDetails();
        IEnumerable<BooksDetails> GetBookDetailsByBookName(string bookName);
        IEnumerable<BooksDetails> GetBookDetailsByBookId(int bookid);
    }
}
