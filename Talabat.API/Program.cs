
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using talabat.Core.Models.Identity;
using talabat.Modeles.Reposituory;
using Talabat.API.Errors;
using Talabat.API.Extension;
using Talabat.API.Helpers;
using Talabat.API.MiddelWares;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Tamara.Net.SDK.Consumer;
using Tamara.Net.SDK.Notification;

namespace Talabat.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            //configration =>ماسكة الفايل بتاع appsetting كله
            builder.Services.AddDbContext<AppIdentityDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(Option =>
            {
                var connection=
                builder.Configuration.GetConnectionString("RedisConnection");
              return  ConnectionMultiplexer.Connect(connection);
            }); 
            builder.Services.AddApplicationServices();


            builder.Services.IdentityServices(builder.Configuration);
            builder.Services.AddHttpClient();
            builder.Services.AddCors(
                Option =>
                {
                    Option.AddPolicy("MyPolicy", option =>
                    {
                        option.AllowAnyHeader();
                        option.AllowAnyMethod();
                       // option.WithOrigins(builder.Configuration["FrontBaseUrl"]);
                       option.AllowAnyOrigin();
                    });
                });
            var app = builder.Build();
            app.MapControllers();


            using var Scope=app.Services.CreateScope();
            //GroupOfServices LifeTimeScoped
            var Services=Scope.ServiceProvider;
            //services it self
            var loggerServices=Services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbcontext = Services.GetRequiredService<StoreContext>();
                
                await dbcontext.Database.MigrateAsync();
                
                var IdentityDbcontext=Services.GetRequiredService<AppIdentityDbContext>();
                
                await IdentityDbcontext.Database.MigrateAsync();

                var usermanager = Services.GetRequiredService<UserManager<AppUser>>();
                
                await AppIdentityDbContextSeed.SeedUserAsync(usermanager);
                
                await StoreContextSeed.SeedAsync(dbcontext);
            }
            catch (Exception ex) 
            { 
                var logger=loggerServices.CreateLogger<Program>();
                logger.LogError(ex, "An Error When Update Database");
            }

            #region dataseeding


            #endregion


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
               // app.UseMiddleware<ExceptionMiddleWare>();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
