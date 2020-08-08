using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreCommonLayer;
using BookStoreCommonLayer.BookDetail;
using BookStoreManagerLayer.IManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookStoreController : ControllerBase
    {
        public IBookStoreManager _Manager;
        public BookStoreController(IBookStoreManager manager)
        {
            this._Manager = manager;
        }

        [HttpPost]
        public IActionResult AddBookDetails(BooksDetails booksDetail)
        {
            string message;
            var result = this._Manager.AddBookDetails(booksDetail);
            try
            {
                if (!result.Equals(null))
                {
                    message = "Book details added successfully.";
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
        public IActionResult GetAllBooksDetails()
        {
            string message;
            var result = this._Manager.GetBookDetails();
            try
            {
                if (!result.Equals(null))
                {
                    message = "All books Detail";
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

        [HttpGet]
        [Route("GetBookDetailsByBookName")]
        public IActionResult GetBookDetailsByBookName(string bookName)
        {
            string message;
            var result = this._Manager.GetBookDetailsByBookName(bookName);
            try
            {
                if (!result.Equals(null))
                {
                    message = " Books Detail";
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

        [HttpGet]
        [Route("GetBookDetailsById")]
        public IActionResult GetBookDetailsById(int bookid)
        {
            string message;
            var result = this._Manager.GetBookDetailsByBookId(bookid);
            try
            {
                if (!result.Equals(null))
                {
                    message = " Books Detail";
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