using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SimpleSocialNetwork.Models;

namespace SimpleSocialNetwork
{
    public static class RoleInitializer
    {
         public static async Task InitializeAsync(UserManager<User> userManager,
                                                  RoleManager<IdentityRole> roleManager,
                                                  IConfiguration configuration)
        {
            string adminEmail = configuration.GetSection("AdminData")["Email"];
            string password = configuration.GetSection("AdminData")["password"];
            string name = "Админ";
            string surname = "Админович";

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("moderator") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("moderator"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail,
                                        Name = name, Surname = surname,
                                        Gender = Gender.Male };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                    await userManager.AddToRoleAsync(admin, "moderator");
                }
            }
        }
    }
}
