using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Evidence_Work_01.ViewModels
{
    public class LoginModel
    {   [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}