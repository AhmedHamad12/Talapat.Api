using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Models.Order_Aggreate;

namespace talabat.Core.Specificatios.OrderSpec
{
    public class OrderSpecification :BaseSpecifications<Order>
    {
        public OrderSpecification(string email) : base(o=>o.BuyerEmaiil==email) 
        { 
            Includes.Add(o=>o.DeliveryMethod);
            Includes.Add(o=>o.Items);
            AddOrderByDes(o=>o.OrderDate);
        }
        public OrderSpecification(string email,int orderId):base(o=>o.BuyerEmaiil == email&&o.id==orderId)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
        }
    }
}
