using talabat.Modeles.Models;

namespace Talabat.API.DTO
{
    public class ProductToReturnDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string pictureUrl { get; set; }
        public decimal price { get; set; }

        public int productBrandId { get; set; } //fk
        public string productBrand { get; set; }
        public int productTypeId { get; set; } //fk

        public string productType { get; set; }
    }
}
