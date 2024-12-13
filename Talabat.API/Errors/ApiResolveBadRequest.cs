namespace Talabat.API.Errors
{
    public class ApiResolveBadRequest :ApiRespones
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiResolveBadRequest():base(400)
        {
            Errors = new List<string>();
        }
    }
}
