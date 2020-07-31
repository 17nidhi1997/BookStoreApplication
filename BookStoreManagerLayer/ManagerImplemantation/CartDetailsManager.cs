using BookStoreManagerLayer.IManager;
using BookStoreRepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManagerLayer.ManagerImplemantation
{
    public class CartDetailsManager : ICartDetailsManager
    {
        private ICartDetialsRepository _BookStore;
        public CartDetailsManager(ICartDetialsRepository BookStore)
        {
            _BookStore = BookStore;
        }
        public object AddCartDetails(int bookid)
        {
            return this._BookStore.AddCartDetails(bookid);
        }

        public object GetCartItems()
        {
            return this._BookStore.GetCartItems();
        }
    }
}
