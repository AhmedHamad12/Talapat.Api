using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Modeles.Models;

namespace talabat.Core.Models.Order_Aggreate
{
    public class OrderItem :BaseEntity
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrder product, decimal price, int quentity)
        {
            Product = product;
            Price = price;
            Quentity = quentity;
        }

        public ProductItemOrder  Product { get; set; }
        public decimal Price { get; set; }
        public int Quentity { get; set; }
    }
}
