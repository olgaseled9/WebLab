using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebLab.Data;
using WebLab.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebLab.Services;

var builder = WebApplication.CreateBuilder(args);

// Подключение БД
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация Identity с UserManager, RoleManager
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Политика авторизации "admin"
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin", policy =>
        policy.RequireClaim(ClaimTypes.Role, "admin"));
});

// Используем ваш класс NoOpEmailSender
builder.Services.AddSingleton<IEmailSender, WebLab.Services.NoOpEmailSender>();

// Настройка MVC и Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Миграции и прочее...
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInit.SeedData(services);
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();
app.Run();

builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();

builder.Services.AddHttpContextAccessor();
