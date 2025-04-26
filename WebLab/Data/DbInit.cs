using Microsoft.AspNetCore.Identity;
using WebLab.Models;

namespace WebLab.Data
{
    public static class DbInit
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Проверка наличия роли "admin"
            var role = await roleManager.FindByNameAsync("admin");
            if (role == null)
            {
                role = new IdentityRole("admin");
                await roleManager.CreateAsync(role);
            }

            // Проверка наличия пользователя с ролью "admin"
            var user = await userManager.FindByNameAsync("admin@admin.com");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com"
                };
                await userManager.CreateAsync(user, "AdminPassword123!");

                // Добавление роли "admin" пользователю
                await userManager.AddToRoleAsync(user, "admin");
            }
        }
    }
}