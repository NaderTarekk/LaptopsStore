//using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class UserLoginModel
    {
        [EmailAddress(ErrorMessage = "Enter Valid Email")]
        public string Email { get; set; }
        public string Password { get; set; }
        //[ValidateNever]
        //public string ReturnUrl { get; set; }
        //[Required]
        //public string Location { get; set; }
    }
}
