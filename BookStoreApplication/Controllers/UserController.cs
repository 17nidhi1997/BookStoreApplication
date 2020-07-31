
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookStoreCommonLayer;
using BookStoreManagerLayer.IManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IAccountManager _Manager;
        private readonly IConfiguration config;
        public UserController(IAccountManager manager, IConfiguration config)
        {
            this.config = config;
            this._Manager = manager;
        }

        [HttpPost]
        [Route("AddUser")]
        public IActionResult AddUserDetails(Registration userDetails)
        {
            string message;
            var item = this._Manager.AddUserDetails(userDetails);
            try
            {
                if (!item.Equals(null))
                {
                    message = "Successfully registred.";
                    return this.Ok(new { message, item });
                }
                message = "Please give proper user details and try again!!";
                return BadRequest(new { message });
            }
            catch (CustomException)
            {
                return BadRequest(CustomException.ExceptionType.INVALID_INPUT);
            }
        }
          
        [HttpPost]
        public IActionResult Login(string email,string password)
        {
            string message,jsonToken;
            bool result = this._Manager.Login(email, password);
            try
            {
                if (result)
                {
                    jsonToken = GenerateToken(email, password, "User");
                    message = "Login done successfully.";
                    return this.Ok(new { result, message,jsonToken });
                }
                message = "Userid or Passward is incorract";
                return BadRequest(new { message });
            }
            catch (CustomException)
            {
                return BadRequest(CustomException.ExceptionType.INVALID_INPUT);
            }
        }

        private string GenerateToken(string email,string password, string type)
        {
            try
            {
                var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));

                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("Email",email));
                claims.Add(new Claim("Password",password));
                var token = new JwtSecurityToken(config["Jwt:Issuer"],
                    config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddHours(120),
                    signingCredentials: signingCreds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
