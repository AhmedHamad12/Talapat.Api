namespace Talabat.API.DTO.TamaraDto
{
    public class PaymentRequestDto
    {
        public decimal TotalAmount { get; set; }
        public string Description { get; set; }
        public string PaymentType { get; set; } // مثل "PAY_BY_INSTALMENTS"
        public List<ItemDto> Items { get; set; }
        public ConsumerDto Consumer { get; set; }
    }
}
