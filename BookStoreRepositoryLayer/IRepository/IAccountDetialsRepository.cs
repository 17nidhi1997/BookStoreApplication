using BookStoreCommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepositoryLayer
{
    public interface IAccountDetialsRepository
    {    
        object AddUserDetails(Registration userDetails);
        bool Login(string userid, string password);
    }
}
