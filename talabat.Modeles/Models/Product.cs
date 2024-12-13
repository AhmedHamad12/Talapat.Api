using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talabat.Modeles.Models
{
    public class Product : BaseEntity
    {
        public string name { get; set; }
        public string description { get; set; }
        public string pictureUrl { get; set; }
        public decimal price { get; set; }

        public int ProductBrandId  { get; set; } //fk
        public ProductBrand productBrand { get; set; }
        public int ProductTypeId { get; set; } //fk

        public ProductType productType { get; set; }
    }
}
