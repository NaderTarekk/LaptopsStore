using Microsoft.AspNetCore.Identity;

namespace ecommerce.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public byte[] ImageData { get; set; }


    }
}
