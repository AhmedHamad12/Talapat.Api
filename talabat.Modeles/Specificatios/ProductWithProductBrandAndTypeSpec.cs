using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Modeles.Models;

namespace talabat.Core.Specificatios
{
   public class ProductWithProductBrandAndTypeSpec :BaseSpecifications<Product>
    {
        public ProductWithProductBrandAndTypeSpec(productSpecParams Params ):base
            
            (p=>
            (string.IsNullOrEmpty(Params.Search)||p.name.ToLower().Contains(Params.Search))
            &&
            (!Params.productBrand.HasValue||p.ProductBrandId==Params.productBrand)
            &&
            (!Params.productType.HasValue||p.ProductTypeId == Params.productType)
            )
        {
            Includes.Add(p => p.productBrand);
            Includes.Add(p => p.productType);
            if(!string.IsNullOrEmpty(Params.sort))
            {
                switch (Params.sort) 
                {
                    case "Price":
                        AddOrderBy(p => p.price);
                            break;
                    case "PriceDes":
                        AddOrderByDes(p => p.price);
                        break;
                    default:
                        AddOrderBy(p=>p.name);
                        break;
                }
            }
            ApplyPagination(Params.PageSize * (Params.pageIndex - 1), Params.PageSize);
        }
        public ProductWithProductBrandAndTypeSpec(int id):base(p=>p.id==id)
        {
            Includes.Add(p => p.productBrand);
            Includes.Add(p => p.productType);
        }
    }
}
