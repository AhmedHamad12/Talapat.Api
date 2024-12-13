using talabat.Core.Models;

namespace Talabat.API.DTO
{
    public class CustomerBasketDto
    {
        public string id { get; set; }
        public List<BasketItemDto> items { get; set; }
        public string? paymentIntentId { get; set; }
        public string? clientSecret { get; set; }
        public int? deliveryMethodId { get; set; }

    }
}
