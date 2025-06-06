using Microsoft.AspNetCore.Identity;

namespace WebLab.Models
{
    public class ApplicationUser : IdentityUser
    {
        public byte[]? AvatarImage { get; set; }
        public string? AvatarContentType { get; set; }
    }

}