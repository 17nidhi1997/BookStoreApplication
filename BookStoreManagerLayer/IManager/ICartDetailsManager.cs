using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManagerLayer.IManager
{
    public interface ICartDetailsManager
    {
        object AddCartDetails(int bookid);
        object GetCartItems();
    }
}
