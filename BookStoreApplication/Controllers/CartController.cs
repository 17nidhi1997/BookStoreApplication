using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreCommonLayer;
using BookStoreManagerLayer.IManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        public ICartDetailsManager _Manager;
        public CartController(ICartDetailsManager manager)
        {
            this._Manager = manager;
        }

        [HttpPut]
        [Route("AddCartDetails")]
        public IActionResult AddCartDetails(int bookid)
        {
            string message;
            var result = this._Manager.AddCartDetails(bookid);
            try
            {
                if (!result.Equals(null))
                {
                    message = "Book added to cart successfully.";
                    return this.Ok(new { message, result });
                }
                message = "Please insert correct book details.!!";
                return BadRequest(new { message });
            }
            catch (CustomException)
            {
                return BadRequest(CustomException.ExceptionType.INVALID_INPUT);
            }
        }

        [HttpGet]
        public IActionResult GetAllCartItems()
        {
            string message;
            var result = this._Manager.GetCartItems();
            try
            {
                if (!result.Equals(null))
                {
                    message = "All Cart Items";
                    return this.Ok(new { message, result });
                }
                message = "Something went wrong please try again!!";
                return BadRequest(new { message });
            }
            catch (CustomException)
            {
                return BadRequest(CustomException.ExceptionType.OPTIONS_NOT_MATCH);
            }
        }
    }
}