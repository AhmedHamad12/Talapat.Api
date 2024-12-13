using System.ComponentModel.DataAnnotations;

namespace Talabat.API.DTO
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string  email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
