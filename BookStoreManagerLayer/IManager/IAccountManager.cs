using BookStoreCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManagerLayer.IManager
{
    public interface IAccountManager
    {
        object AddUserDetails(Registration userDetails);
        bool Login(string email, string password);
    }
}
