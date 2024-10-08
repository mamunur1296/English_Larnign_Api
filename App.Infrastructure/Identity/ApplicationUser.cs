using Microsoft.AspNetCore.Identity;


namespace App.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ?Latitude { get; set; }
        public string ?Longitude { get; set; }
    }
}
