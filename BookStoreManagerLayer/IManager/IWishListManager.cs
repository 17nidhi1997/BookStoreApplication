using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManagerLayer.IManager
{
    public interface IWishListManager
    {
        object AddWishListDetails(int bookid);
        object GetWishListItems();
    }
}
