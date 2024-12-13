using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace talabat.Core.Models
{
    public class CustomerBasket
    {
        public CustomerBasket(string id)  
        {  
            this.id= id;
        }
        public string id { get; set; }
        public List<BasketItem> items { get; set; }
        public string paymentIntentId { get; set; }
        public string  clientSecret { get; set; }
        public int? deliveryMethodId { get; set; }


    }
}
