using BookStoreCommonLayer.BookDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepositoryLayer.IRepository
{
    public interface IWishListRepository
    {
        object AddWishListDetails(int bookid);
        IEnumerable<BooksDetails> GetWishListItems();
    }
}
