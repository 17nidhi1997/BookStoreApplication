using BookStoreManagerLayer.IManager;
using BookStoreRepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManagerLayer.ManagerImplemantation
{
    public class WishListManager : IWishListManager
    {
        private IWishListRepository _BookStore;
        public WishListManager(IWishListRepository BookStore)
        {
            _BookStore = BookStore;
        }
        public object AddWishListDetails(int bookid)
        {
            return this._BookStore.AddWishListDetails(bookid);
        }

        public object GetWishListItems()
        {
            return this._BookStore.GetWishListItems();
        }
    }
}
