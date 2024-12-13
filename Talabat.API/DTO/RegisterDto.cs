using System.ComponentModel.DataAnnotations;

namespace Talabat.API.DTO
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string displayName { get; set; }
        //[Required]
        //[Phone]
      //  public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string password { get; set; }
    }
}
