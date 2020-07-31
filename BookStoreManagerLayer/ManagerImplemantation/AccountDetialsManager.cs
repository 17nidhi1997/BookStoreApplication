using BookStoreCommonLayer;
using BookStoreManagerLayer.IManager;
using BookStoreRepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManagerLayer.ManagerImplemantation
{
    public class AccountDetialsManager: IAccountManager
    {
        private IAccountDetialsRepository _AccountDeatials;
        public AccountDetialsManager(IAccountDetialsRepository BookStore)
        {
            _AccountDeatials = BookStore;
        }
        public object AddUserDetails(Registration userDetails)
        {
            return this._AccountDeatials.AddUserDetails(userDetails);
        }
        public bool Login(string email, string password)
        {
            return this._AccountDeatials.Login(email, password);
        }
    }
}
