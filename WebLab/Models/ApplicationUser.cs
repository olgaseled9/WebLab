using Microsoft.AspNetCore.Identity;

namespace WebLab.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}