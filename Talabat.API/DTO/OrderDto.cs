using System.ComponentModel.DataAnnotations;
using talabat.Core.Models.Order_Aggreate;

namespace Talabat.API.DTO
{
    public class OrderDto
    {
        [Required]  
        public string basketId { get; set; }
        public int deliveryMethodId { get; set; }
        public AddressDto shippingAddress { get; set; }

    }
}
