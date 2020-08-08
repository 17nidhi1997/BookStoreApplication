using BookStoreCommonLayer.BookDetail;
using BookStoreManagerLayer.IManager;
using BookStoreRepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManagerLayer.ManagerImplemantation
{
    public class BookStoreManager : IBookStoreManager
    {
        private IBookDetialsRepository _BookStore;
        public BookStoreManager(IBookDetialsRepository BookStore)
        {
            _BookStore = BookStore;
        }
        public object AddBookDetails(BooksDetails booksDetail)
        {
            return this._BookStore.AddBookDetails(booksDetail);
        }
        public object GetBookDetails()
        {
            return this._BookStore.GetBookDetails();
        }

        public object GetBookDetailsByBookId(int bookid)
        {
            return this._BookStore.GetBookDetailsByBookId(bookid);
        }

        public object GetBookDetailsByBookName(string bookName)
        {
            return this._BookStore.GetBookDetailsByBookName(bookName);
        }
    }
}
