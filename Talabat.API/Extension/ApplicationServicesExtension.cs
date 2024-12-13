using Microsoft.AspNetCore.Mvc;
using Talabat.API.Helpers;
using talabat.Modeles.Reposituory;
using Talabat.Repository;
using Talabat.API.Errors;
using talabat.Core.Repository;
using talabat.Core;
using Talabat.Service;
using talabat.Core.Services;

namespace Talabat.API.Extension
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
           // Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            //builder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfile()));
           Services.AddAutoMapper(typeof(MappingProfile));
            Services.AddScoped(typeof(IBasketRepository),typeof(BasketRepository));
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<IpaymentService, PaymentService>();
            Services.AddSingleton<IResponseCacheService, CashingService>();

            Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(d => d.Value.Errors.Count() > 0)
                                                       .SelectMany(p => p.Value.Errors)
                                                       .Select(p => p.ErrorMessage)
                                                       .ToArray();
                    var validationErrorResponse = new ApiResolveBadRequest()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });
            return Services;
        }
       
    }
}
