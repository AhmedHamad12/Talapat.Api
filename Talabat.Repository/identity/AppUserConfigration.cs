//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using talabat.Core.Models.Identity;

//namespace Talabat.Repository.Identity
//{
//    internal class AppUserConfigration : IEntityTypeConfiguration<AppUser>
//    {
//        public void Configure(EntityTypeBuilder<AppUser> builder)
//        {
//            var passwordHaser = new PasswordHasher<AppUser>();




//            builder.HasData(

//                new AppUser()
//                {
//                    Id = "45F555CF-1EE9-4271-A9A1-E74405576922",
//                    DisplayName = "AhmedHamad",
//                    Email = "ahmedhamad9855@gmail.com",
//                    UserName = "ahmedhamad9855",
//                    PhoneNumber = "01094870104",
//                    PasswordHash = passwordHaser.HashPassword(null, "ahmed")
//                }
//                );


//        }
//    }
//}
