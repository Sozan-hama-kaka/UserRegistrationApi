using Microsoft.AspNetCore.Identity;

namespace UserRegistrationApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string Name { get; set; } = "";

    }
}
