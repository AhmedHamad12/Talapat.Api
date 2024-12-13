
namespace Talabat.API.Errors
{
    public class ApiRespones
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public ApiRespones(int statuscode,string? errormessage=null)
        {
            StatusCode=statuscode;
            ErrorMessage = errormessage??GetDeafultMessageForStatusCode(StatusCode);
        }

        private string? GetDeafultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "BadRequest",
                401=>"unhothrized",
                404=>"NotFound",
                500=>"internalServerError",
                _=>null
            };
        }
    }
}
