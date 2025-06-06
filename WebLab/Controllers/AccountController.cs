using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebLab.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.StaticFiles;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // Страница входа
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null && await _userManager.CheckPasswordAsync(user, password))
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home"); // Перенаправление на главную страницу после входа
        }
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View();
    }

    // Страница регистрации
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string email, string username, string password, string confirmPassword)
    {
        if (password != confirmPassword)
        {
            ModelState.AddModelError(string.Empty, "Passwords do not match.");
            return View();
        }

        var user = new ApplicationUser { UserName = username, Email = email };
        var result = await _userManager.CreateAsync(user, password);
        
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home"); // Перенаправление на главную страницу после регистрации
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> UploadAvatar(IFormFile avatarFile)
    {
        if (avatarFile != null && avatarFile.Length > 0)
        {
            using var ms = new MemoryStream();
            await avatarFile.CopyToAsync(ms);
            var user = await _userManager.GetUserAsync(User);
            user.AvatarImage = ms.ToArray();

            var provider = new FileExtensionContentTypeProvider();
            provider.TryGetContentType(avatarFile.FileName, out string? contentType);
            user.AvatarContentType = contentType ?? "application/octet-stream";

            await _userManager.UpdateAsync(user);
        }
        return RedirectToAction("Index", "Home");
    }

    // Выход
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
