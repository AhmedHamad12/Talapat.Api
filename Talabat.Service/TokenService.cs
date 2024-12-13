using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Models.Identity;
using talabat.Core.Services;

namespace Talabat.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser user,UserManager<AppUser> usermanager)
        {
            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName,user.DisplayName),
                new Claim(ClaimTypes.Email,user.Email),

            };
            var UseRole = await usermanager.GetRolesAsync(user);
            foreach (var Role in UseRole) 
            {
             AuthClaims.Add ( new Claim(ClaimTypes.Role, Role));
            }
            var AutKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
           
            var Token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidationIssuer"],
                audience: configuration["JWT:ValidationAudience"],
                expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDays"])),
                claims: AuthClaims,
               signingCredentials: new SigningCredentials(AutKey, SecurityAlgorithms.HmacSha256)

                );
            

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
