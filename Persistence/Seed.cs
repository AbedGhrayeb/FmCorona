using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Persistence
{
    public static class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            if (!await userManager.Users.AnyAsync())
            {
                var user = new AppUser
                {
                    UserName = "admin@domain.com",
                    Email = "admin@domain.com",
                    LockoutEnabled = false
                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}
