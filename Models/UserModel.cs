//using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Enter Valid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
        //[ValidateNever]
        //public string ReturnUrl { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
