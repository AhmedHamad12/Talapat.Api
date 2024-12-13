using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Models.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> usermanager)
        {
            if (!usermanager.Users.Any())
            {
                var user = new AppUser()
                {
                    

                    DisplayName = "AhmedHamad",
                    Email = "ahmedhamad9855@gmail.com",
                    UserName = "ahmedhamad9855",
                    PhoneNumber = "01094870104"
                };
                await usermanager.CreateAsync(user, "A7medHamad9855@!");
                
            }
        }
    }
}