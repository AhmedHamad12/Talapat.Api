using Tamara.Net.SDK.Models.Order;

namespace Talabat.API.DTO.TamaraDto
{
    public class PaymentRequestModel :Order
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
    }
}
