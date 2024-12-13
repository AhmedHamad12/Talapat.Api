using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using talabat.Core.Services;

namespace Talabat.API.Helpers
{
    public class CashedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int expireTime;

        public CashedAttribute(int ExpireTime)
        {
            expireTime = ExpireTime;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();//ask Clr To inject Explicitly
            var cashKey= GenerateCashKeyFromRequest(context.HttpContext.Request);
            var cachedResponse = await cacheService.GetCashResponse(cashKey);
            if(!string.IsNullOrEmpty(cachedResponse) )
            {
                var ContentResult = new ContentResult()
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = ContentResult;
                return;
            }
            var ExcutedEndPointContext= await next.Invoke();
            if( ExcutedEndPointContext.Result is OkObjectResult result )
            {
              await cacheService.CashResponse(cashKey,result.Value,TimeSpan.FromSeconds(expireTime));
            }
        }
        private string GenerateCashKeyFromRequest(HttpRequest request) 
        {
            var KeyBuilder = new StringBuilder();
            KeyBuilder.Append(request.Path);
            foreach(var (Key,Value)in request.Query.OrderBy(x=>x.Key))
            {
                KeyBuilder.Append($"|{Key}-{Value}");
            }
            return KeyBuilder.ToString();
            
        }

    }
}
