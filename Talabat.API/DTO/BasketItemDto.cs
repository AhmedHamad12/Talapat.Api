using System.ComponentModel.DataAnnotations;

namespace Talabat.API.DTO
{
    public class BasketItemDto
    {
        [Required]
        
        public int id { get; set; }
        
        public string? name { get; set; }
        [Required]
        public string pictureUrl { get; set; }
        [Required]
        public string brand { get; set; }
        [Required]
        public string type { get; set; }

        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage = "price Can't be zero")]
        public decimal price { get; set; }

        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Quantity Can't be zero")]
        public int quantity { get; set; }
    }
}