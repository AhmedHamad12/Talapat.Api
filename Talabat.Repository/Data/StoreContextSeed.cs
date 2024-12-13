using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using talabat.Core.Models.Order_Aggreate;
using talabat.Modeles.Models;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbcontext)
        {
            if (!dbcontext.productBrands.Any())
            {
                var brandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var brand = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                if (brand?.Count > 0)
                {
                    foreach (var item in brand)
                    {
                        await dbcontext.Set<ProductBrand>().AddAsync(item);
                    }
                    await dbcontext.SaveChangesAsync();
                }
            }
            if (!dbcontext.productTypes.Any())
            {
                var ProductTypes = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var type = JsonSerializer.Deserialize<List<ProductType>>(ProductTypes);
                if (type?.Count > 0)
                {
                    foreach (var item in type)
                    {
                        await dbcontext.Set<ProductType>().AddAsync(item);
                    }
                    await dbcontext.SaveChangesAsync();
                }
            }
            if (!dbcontext.products.Any())
            {
                var products = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var product = JsonSerializer.Deserialize<List<Product>>(products);
                if (product?.Count > 0)
                {
                    foreach (var item in product)
                    {
                        await dbcontext.Set<Product>().AddAsync(item);
                    }
                    await dbcontext.SaveChangesAsync();
                }
            }
            if (!dbcontext.DeliveryMethods.Any())
            {
                var DeliveryMethodData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var JsonDelivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodData);
                if (JsonDelivery?.Count > 0)
                {
                    foreach (var item in JsonDelivery)
                        await dbcontext.Set<DeliveryMethod>().AddAsync(item);
                }
                await dbcontext.SaveChangesAsync();
            }
        }
    }
}
