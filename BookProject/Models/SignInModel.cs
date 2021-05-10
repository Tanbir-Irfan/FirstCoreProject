using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookProject.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Please enter your email"), EmailAddress(ErrorMessage = "Please enter correct email format address"), Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your password"), Display(Name = "Password"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}