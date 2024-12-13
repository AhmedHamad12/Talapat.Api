using System.Net;
using System.Text.Json;
using Talabat.API.Errors;

namespace Talabat.API.MiddelWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _env;

        //RequestDelegate=>is a pointer  ماسك middleWarew الى بعد الى واقف عليها 
        //IHostEnvironment=> بيجيب احنا فى انهي بيئة دلوقتي سوا فى {Development or production}
        //ILogger =>بيعمل مسدج ايرور يدوى 

        public ExceptionMiddleWare(RequestDelegate Next ,ILogger<ExceptionMiddleWare> logger,IHostEnvironment env)
        {
            _next = Next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
              await  _next.Invoke(context);
            }
            catch(Exception ex) 
            { 
                _logger.LogError(ex,ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
              ///  if (_env.IsDevelopment())
              ///  {
              ///      var response = new InternalServerError(500, ex.Message, ex.StackTrace.ToString());
              ///  }
              ///  else
              ///  {
              ///      var response = new InternalServerError(500);
              ///  }
               
                var Response=_env.IsDevelopment()? new InternalServerError(500, ex.Message, ex.StackTrace.ToString()) : new InternalServerError(500);
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };
                var jsonresopnse=JsonSerializer.Serialize(Response,options);
              await  context.Response.WriteAsync(jsonresopnse);
            }
        }
    }
}
