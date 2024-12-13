using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NBitcoin.Payment;
using talabat.Core.Models;
using talabat.Core.Services;
using Talabat.API.DTO;
using Talabat.API.DTO.TamaraDto;
using Talabat.API.Errors;
using Tamara.Net.SDK.Consumer;
namespace Talabat.API.Controllers
{

    public class PaymentController : APIBaseController
    {
        private readonly IpaymentService _paymentService;
        private readonly IMapper _mapper;
       
        private readonly IConfiguration _configuration;
        private readonly ITamaraApiClient _tamaraApiClient;

        public PaymentController (IpaymentService PaymentService,
            IMapper mapper,  IConfiguration configuration)
        {
            _paymentService = PaymentService;
            _mapper = mapper;
            _configuration = configuration;
          
        }
        [HttpPost]
        [ProducesResponseType(typeof(CustomerBasketDto), 200)]
        [ProducesResponseType(typeof(ApiRespones), 400)]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string BasketId)
        {
            var CustomerBasket = await _paymentService.CreateOrUpdatePaymentIntent(BasketId);
            if (CustomerBasket == null) return BadRequest(new ApiRespones(400, "have problem in basket"));
            var MappedCustomer = _mapper.Map<CustomerBasket, CustomerBasketDto>(CustomerBasket);
            return Ok(MappedCustomer);
        }
    }
}

