using HotelBookingPlatform.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingPlatform.Infrastructure.Data
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var email = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
                var password = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

                var user = new AppUser()
                {
                    DisplayName = "Admin User",
                    UserName = "admin",
                    Email = email,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, password);
            }
        }
    }
}
