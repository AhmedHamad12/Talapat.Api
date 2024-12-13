using talabat.Core.Models.Order_Aggreate;

namespace Talabat.API.DTO
{
    public class OrderToReturnDto
    {
        public int id { get; set; }
        public string buyerEmaiil { get; set; }
        public DateTimeOffset orderDate { get; set; } 
        public string status { get; set; }
        public Address shippingAddress { get; set; }
        public string deliveryMethod { get; set; }
        public decimal deliveryMethodCost { get; set; }
        public ICollection<OrderItemDto> items { get; set; } = new HashSet<OrderItemDto>();
        public decimal subTotal { get; set; }
        public decimal total { get; set; }
        public string paymentIntentId { get; set; } 
    }
}
