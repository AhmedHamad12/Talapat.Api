using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using talabat.Core.Models.Identity;
using talabat.Core.Services;
using Talabat.Repository.Identity;
using Talabat.Service;

namespace Talabat.API.Extension
{
    public static class IdentityServicesExtension
    {
        public static IServiceCollection IdentityServices (this IServiceCollection Services,IConfiguration configuration)
        {
            Services.AddIdentity<AppUser, IdentityRole>()//implement Inferfaces 
                           .AddEntityFrameworkStores<AppIdentityDbContext>();//implement class implement interfaces
            Services.AddScoped<ITokenService, TokenService>();
            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:ValidationIssuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidationAudience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))


                    };
                });//userManager//SignInmanager//RoleManager
            return Services;
        }
    }
}
