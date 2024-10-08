using Microsoft.AspNetCore.Identity;
namespace App.Infrastructure.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() { }
        public ApplicationRole(string roleName) : base(roleName) { }
    }
}
