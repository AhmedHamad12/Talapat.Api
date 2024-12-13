using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using talabat.Core.Models;
using talabat.Core.Repository;
using Talabat.API.DTO;
using Talabat.API.Errors;

namespace Talabat.API.Controllers
{
    
    public class BasketController : APIBaseController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            this._basketRepository = basketRepository;
            this.mapper = mapper;
        }
        [HttpGet("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string basketId) 
        { 
            var Basket=await _basketRepository.GetCustomerAsync(basketId);
            return  Basket is null ? new CustomerBasket(basketId) :Ok( Basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket( CustomerBasketDto Basket)
        {
            var MappedBasket=mapper.Map<CustomerBasketDto,CustomerBasket>(Basket);
            var CreateOrUpdate=await _basketRepository.UpdateCustomerAsync(MappedBasket);
            if (CreateOrUpdate == null)
                return BadRequest(new ApiRespones(400));
            return Ok( CreateOrUpdate );
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string Id)
        {
          return  await _basketRepository.DeleteBasketAsync(Id);

        }
    }
}
