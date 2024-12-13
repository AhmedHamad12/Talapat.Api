using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using talabat.Core.Models.Identity;

namespace Talabat.API.Extension
{
    public static class UseManagerExtension
    {
        public static async Task<AppUser?> FindUserWithAddressAsync(this UserManager<AppUser> userManager,ClaimsPrincipal User)
        {
            var email=User.FindFirstValue(ClaimTypes.Email);
            var user=await userManager.Users.Include(x => x.address).FirstOrDefaultAsync(o=>o.Email==email);

            return user;

        }
    }
}
