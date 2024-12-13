using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core;
using talabat.Core.Models.Order_Aggreate;
using talabat.Core.Repository;
using talabat.Core.Services;
using talabat.Core.Specificatios.OrderSpec;
using talabat.Modeles.Models;
using talabat.Modeles.Reposituory;

namespace Talabat.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IpaymentService _paymentService;

        public OrderService(IBasketRepository basketRepository,IUnitOfWork unitOfWork,IpaymentService paymentService)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            this._paymentService = paymentService;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            //Get Basket  From BasketRepo
            var Basket=await _basketRepository.GetCustomerAsync(BasketId);
            //Get Selected Items at Basket From ProductRepo
            var OrderItems=new List<OrderItem>();
            if (Basket?.items.Count > 0)
            {
                foreach (var item in Basket.items)
                {
                    var Product = await _unitOfWork.repo<Product>().GetByIdAsync(item.Id);
                    var ProductItemOrder = new ProductItemOrder(Product.id, Product.name, Product.pictureUrl);
                    var OrdeerItem = new OrderItem(ProductItemOrder, Product.price, item.Quantity);
                    OrderItems.Add(OrdeerItem);
                }
            }
                //subTotal 
                var SubTotal=OrderItems.Sum(Itm=>Itm.Quentity*Itm.Price);
                //DeliverMethod
                var DeliverMethod=await _unitOfWork.repo<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);
            //Create Order
                var spec = new OrderPaymentIntentIdSpec(Basket.paymentIntentId);
                var ExOrder=await _unitOfWork.repo<Order>().GetByIdWithSpecificationAsync(spec);
                if (ExOrder != null)    
                { 
                    _unitOfWork.repo<Order>().Delete(ExOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(BasketId);
                }
                var Order=new Order(buyerEmail,ShippingAddress,DeliverMethod,OrderItems,SubTotal,Basket.paymentIntentId);
                //Add LocalInDatabase
                await _unitOfWork.repo<Order>().AddAsync(Order);
               var Result= await _unitOfWork.CompleteAsync();
            if (Result <= 0) return null;
                return Order;
            }
            

        

        public async Task<Order> GetOrderByIdFoeSpecificUserAsync(string buyerEmail, int OrderId)
        {
            var spec=new OrderSpecification(buyerEmail,OrderId);
            var Order = await _unitOfWork.repo<Order>().GetByIdWithSpecificationAsync(spec);
            return Order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecification(buyerEmail);
            var Orders=await _unitOfWork.repo<Order>().GetAllWithSpecAsync(spec);
            return Orders;
        }
    }
}
