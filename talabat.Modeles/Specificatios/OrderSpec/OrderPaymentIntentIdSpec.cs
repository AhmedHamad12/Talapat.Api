using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core.Models.Order_Aggreate;

namespace talabat.Core.Specificatios.OrderSpec
{
    public class OrderPaymentIntentIdSpec :BaseSpecifications<Order>
    {
        public OrderPaymentIntentIdSpec(string PaymentIntentId):base(o=>o.PaymentIntentId==PaymentIntentId)
        {
            
        }
    }
}
