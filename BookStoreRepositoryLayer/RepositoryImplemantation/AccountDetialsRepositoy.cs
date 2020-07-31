using BookStoreCommonLayer;
using BookStoreCommonLayer.Queries;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreRepositoryLayer.RepositoryImplemantation
{
    public class AccountDetialsRepositoy : IAccountDetialsRepository
    {
        private readonly IConfiguration configuration;
        public AccountDetialsRepositoy(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public object AddUserDetails(Registration userDetails)
        {
            try
            {
                var commandText = Queries.userDitailsQuery;
                using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                using (OracleCommand cmd = new OracleCommand(commandText, _db))
                {
                    cmd.Connection = _db;
                    cmd.Parameters.Add("UserId", userDetails.UserId);
                    cmd.Parameters.Add("FirstName", userDetails.FirstName);
                    cmd.Parameters.Add("LastName", userDetails.LastName);
                    cmd.Parameters.Add("Email", userDetails.Email);
                    cmd.Parameters.Add("Password", userDetails.Password);
                    _db.Open();
                    cmd.ExecuteNonQuery();
                    _db.Close();
                    return userDetails.UserId + "Registration sucessfull";
                }
            }
            catch (CustomException exception)
            {
                throw new CustomException(CustomException.ExceptionType.INVALID_INPUT, exception.Message);
            }

        }
         public  bool Login(string email,string password)
         {
             var commandTexts = Queries.loginRolesQuery;
             using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
             using (OracleCommand cmd = new OracleCommand(commandTexts, _db))
             {
                cmd.Parameters.Add(new OracleParameter("email", email));
                cmd.Parameters.Add(new OracleParameter("password", password));
                cmd.Connection = _db;
                cmd.CommandType = CommandType.Text;
                _db.Open();
                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    _db.Close();
                    return true;
                }
                return false;
             } 
        }
    }
}