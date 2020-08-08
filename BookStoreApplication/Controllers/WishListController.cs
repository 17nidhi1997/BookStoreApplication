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
    public class WishListController : ControllerBase
    {
        public IWishListManager _Manager;
        public WishListController(IWishListManager manager)
        {
            this._Manager = manager;
        }

        [HttpPut]
        public IActionResult AddWishListDetails(int bookid)
        {
            string message;
            var result = this._Manager.AddWishListDetails(bookid);
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
        public IActionResult GetWishListItems()
        {
            string message;
            var result = this._Manager.GetWishListItems();
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