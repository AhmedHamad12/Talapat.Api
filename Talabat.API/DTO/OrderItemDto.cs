using talabat.Core.Models.Order_Aggreate;

namespace Talabat.API.DTO
{
    public class OrderItemDto
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string pictureUrl { get; set; }
        public decimal price { get; set; }
        public int quentity { get; set; }
    }
}