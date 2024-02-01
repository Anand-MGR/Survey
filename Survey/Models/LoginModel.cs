using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Survey.Models
{
    public class LoginModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "User name")]
        [StringLength(50, ErrorMessage = "Please limit username to 50 characters")]
       // [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Please enter only alphabets and numbers")]
        public string UserName { get; set; }
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Password is required")]
       // [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Please enter only alphabets and numbers")]
        [StringLength(20, ErrorMessage = "Please limit password to 20 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string Message { get; set; }

        public string UserRole { get; set; }
        public string UR_Role { get; set; }
        public string Images { get; set; }
    }

    public class ChangePassword
    {
        public string OldPassword { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Please enter only alphabets and numbers")]
        [StringLength(20, ErrorMessage = "Please limit password to 20 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("([a-zA-Z0-9 .&'-]+)", ErrorMessage = "Please enter only alphabets and numbers")]
        [StringLength(20, ErrorMessage = "Please limit password to 20 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string CFNewPassword { get; set; }
    }
}