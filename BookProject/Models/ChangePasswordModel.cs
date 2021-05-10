using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookProject.Models
{
    public class ChangePasswordModel
    {
        [Required, DataType(DataType.Password), Display(Name = "Old Password")]
        public string CurrentPassword { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Confirm New Password"), Compare("NewPassword", ErrorMessage = "Confirm Password Does Not Match")]
        public string ConfirmNewPassword { get; set; }
    }
}