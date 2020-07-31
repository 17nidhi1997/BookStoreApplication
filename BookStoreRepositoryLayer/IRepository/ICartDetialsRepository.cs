using BookStoreCommonLayer.BookDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepositoryLayer.IRepository
{
    public interface ICartDetialsRepository
    {     
        object AddCartDetails(int bookid);
        IEnumerable<BooksDetails> GetCartItems();
    }
}
