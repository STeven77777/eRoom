using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;

namespace MetroOil.LoyaltyOps.Models
{
    public class LoginModel
    {
        //[StringLength(20)]
        [Required]
        [DisplayName("Username")]
        public string LoginID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        //[Compare("Password")]
        public string Password { get; set; }

        public string dbName { get; set; }
        public string PasswordHash { get; set; }
        public int Captcha { get; set; }

        public string ReturnUrl { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        //[RegularExpression(@"^(?=.*\d)(?=.*[#$@!%&*?])[A-Za-z\d#$@!%&*?]{8,}$", ErrorMessage = "Password must contain alphanumeric characters with minimum 8 characters")]
        [RegularExpression(@"^(?=.*[A-Za-z])[A-Za-z\d#$@!%&*?]{8,}$", ErrorMessage = "Password must contain alphanumeric characters with minimum 8 characters.")]
        [Compare("NewPassword", ErrorMessage = "New password and confirm password mismatch")]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "New password and confirm password mismatch")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string OldPassword { get; set; }

        [Required]
        [RegularExpression(@"^[\d]{4}$", ErrorMessage = "Incorrect verification code format.")]
        [Display(Name = "Verification Code")]
        public string VerificationCode { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Incorrect email format.")]
        public string EmailAddress { get; set; }

        public int UserType { get; set; }
        public int DeviceType { get; set; }
        public string DeviceId { get; set; }
        public string AppsVersionCd { get; set; }
        public string AppsVersionName { get; set; }
    }
}