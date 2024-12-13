using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Modeles.Models;

namespace talabat.Core.Models.Order_Aggreate
{
    public class Order :BaseEntity
    {
        public Order()
        {
            
        }
        public Order(string buyerEmaiil, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal,string paymentIntentId)
        {
            BuyerEmaiil = buyerEmaiil;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string  BuyerEmaiil { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; }=OrderStatus.Pending;
        public Address ShippingAddress { get; set; }
       // public int DeliveryMethodId { get; set; }//FK it now one to many to make oit one to one must put uniqe Constrains
        public DeliveryMethod  DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public decimal GetTotal ()=> SubTotal+DeliveryMethod.Cost;

        public string PaymentIntentId { get; set; } 

    }
}
