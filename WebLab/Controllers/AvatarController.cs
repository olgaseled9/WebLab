using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebLab.Models;

namespace WebLab.Controllers;

public class AvatarController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IWebHostEnvironment _env;

    public AvatarController(UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
    {
        _userManager = userManager;
        _env = env;
    }

    public async Task<IActionResult> GetImage()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user?.AvatarImage != null && !string.IsNullOrEmpty(user.AvatarContentType))
        {
            return File(user.AvatarImage, user.AvatarContentType);
        }

        var defaultPath = Path.Combine(_env.WebRootPath, "images", "default-avatar.png");
        var imageBytes = await System.IO.File.ReadAllBytesAsync(defaultPath);
        return File(imageBytes, "image/png");
    }
}
