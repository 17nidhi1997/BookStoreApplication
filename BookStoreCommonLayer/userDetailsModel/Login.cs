using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookStoreCommonLayer
{
    public class Login
    {       
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
