namespace Talabat.API.Errors
{
    public class InternalServerError :ApiRespones
    {
        public string Details { get; set; }
        public InternalServerError(int statusCode,string? message=null,string? details=null):base(statusCode,message)
        {
            Details = details;
        }
    }
}
