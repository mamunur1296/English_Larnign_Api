using App.Application.Common;

namespace App.Application.DTOs
{
    public class UserDTO : BaseDTOs
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public List<string> Roles { get; set; }
    }
}
