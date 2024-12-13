using System.ComponentModel.DataAnnotations;

namespace Talabat.API.DTO
{
    public class AddressDto
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string street { get; set; }
        [Required]
        public string country { get; set; }
    }
}
