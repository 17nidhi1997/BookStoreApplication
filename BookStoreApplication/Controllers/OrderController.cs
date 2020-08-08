using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreCommonLayer;
using BookStoreCommonLayer.BookDetail;
using BookStoreCommonLayer.BookModel;
using BookStoreManagerLayer.IManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IOrderDetialsManager _Manager;
        public OrderController(IOrderDetialsManager manager)
        {
            this._Manager = manager;
        }

        [HttpPost]
        public IActionResult PlaceOrder(OrderDetails orderDetail)
        {
            string message;
            var result = this._Manager.PlaceOrder(orderDetail);
            try
            {
                if (!result.Equals(null))
                {
                    message = "placed Order successfully.";
                    _Manager.OrderConfirmationToEmail(orderDetail.UUserId, orderDetail.OrderId);
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
        public IActionResult ViewOrderPlaced()
        {
            string message;
            var result = this._Manager.ViewOrderPlaced();
            try
            {
                if (!result.Equals(null))
                {
                    message = "view order details";
                    return this.Ok(new { message, result });
                }
                message = "Something went wrong";
                return BadRequest(new { message });
            }
            catch (CustomException)
            {
                return BadRequest(CustomException.ExceptionType.INVALID_INPUT);
            }
        }
    }
}