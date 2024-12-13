using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Modeles.Models;

namespace talabat.Core.Specificatios
{
    public  class ProductFilterationToGetCountAsync:BaseSpecifications<Product>
    {
        public ProductFilterationToGetCountAsync(productSpecParams Params):
            base(p =>
            (string.IsNullOrEmpty(Params.Search) || p.name.ToLower().Contains(Params.Search))
            &&
            (!Params.productBrand.HasValue || p.ProductBrandId == Params.productBrand)
            &&
            (!Params.productType.HasValue || p.ProductTypeId == Params.productType)
            )
        {
            
        }
    }
}
