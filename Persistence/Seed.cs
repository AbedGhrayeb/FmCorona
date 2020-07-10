using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Persistence
{
    public static class Seed
    {
        public static async Task SeedData(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            if (!await roleManager.Roles.AnyAsync())
            {
                var admin = new IdentityRole("admin");
                var employee = new IdentityRole("employee");
                var user = new IdentityRole("user");
                await roleManager.CreateAsync(admin);
                await roleManager.CreateAsync(employee);
                await roleManager.CreateAsync(user);
            }
            if (!await userManager.Users.AnyAsync())
            {
                var admin = new AppUser
                {
                    UserName = "admin@domain.com",
                    Email = "admin@domain.com",
                    LockoutEnabled = false
                };
                await userManager.CreateAsync(admin, "Admin.1234");
                var emp = new AppUser
                {
                    UserName = "emp@domain.com",
                    Email = "emp@domain.com",
                    LockoutEnabled = false
                };
                await userManager.CreateAsync(emp, "Emp.1234");
            }
        }
    }
}

