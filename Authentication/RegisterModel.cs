using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Authentication
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Full Name is required!")]
        public string FullName { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

    }
}
