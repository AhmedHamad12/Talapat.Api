using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using talabat.Core;
using talabat.Core.Models;
using talabat.Core.Models.Order_Aggreate;
using talabat.Core.Repository;
using talabat.Core.Services;
using Product = talabat.Modeles.Models.Product;

namespace Talabat.Service
{

    public class PaymentService : IpaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration
            ,IBasketRepository basketRepository
            ,IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            this._unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string BasketId)
        {
            //secrtKey
            StripeConfiguration.ApiKey = _configuration["StripeKeys:Secretkey"];
            //GetBasket
            var basket=await _basketRepository.GetCustomerAsync(BasketId);
            if (basket == null) return null;
            var shippingPrice = 0m;
            if (basket.deliveryMethodId.HasValue)
            {
                var DeliveryMethod=await _unitOfWork.repo<DeliveryMethod>().GetByIdAsync(basket.deliveryMethodId.Value);
                 shippingPrice = DeliveryMethod.Cost;
            }
            if (basket.items.Count > 0)
            {
                foreach (var item in basket.items) 
                { 
                    var product= await _unitOfWork.repo<Product>().GetByIdAsync(item.Id);
                    if (product != null)
                    {
                        if (item.Price != product.price)
                            item.Price = product.price;
                    }
                }
            }
            
            var SubTotal=basket.items.Sum(item=>item.Price*item.Quantity);
            //Create PaymentIntent
            var service=new PaymentIntentService();
            PaymentIntent paymentIntent;
            if(string.IsNullOrEmpty(basket.paymentIntentId))//create
            {
                var option = new PaymentIntentCreateOptions()
                {
                    Amount = (long)(shippingPrice * 100 + SubTotal * 100),
                    Currency = "usd",
                    PaymentMethodTypes=new List<string>() { "card"}
                };
                paymentIntent=await service.CreateAsync(option);
                basket.paymentIntentId=paymentIntent.Id;
                basket.clientSecret=paymentIntent.ClientSecret;
            }
            else //update
            {
                var option = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(shippingPrice * 100 + SubTotal * 100),

                };
                paymentIntent= await  service.UpdateAsync(basket.paymentIntentId, option);
                basket.paymentIntentId = paymentIntent.Id;
                basket.clientSecret = paymentIntent.ClientSecret;
            }
           await _basketRepository.UpdateCustomerAsync(basket);
            return basket;

        }
    }
}
