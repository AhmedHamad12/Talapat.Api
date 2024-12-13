using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using talabat.Core;
using talabat.Core.Models.Order_Aggreate;
using talabat.Core.Services;
using Talabat.API.DTO;
using Talabat.API.Errors;
using Talabat.Service;

namespace Talabat.API.Controllers
{
    
    public class OrdersController : APIBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IOrderService orderService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(Order),statusCode:200)]
        [ProducesResponseType(typeof(ApiRespones), statusCode: 400)]
        public async Task <ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var BuyerEmail=User.FindFirstValue(ClaimTypes.Email);
            var Maped = _mapper.Map<AddressDto, Address>(orderDto.shippingAddress);
            var Order =await _orderService.CreateOrderAsync(BuyerEmail, orderDto.basketId, orderDto.deliveryMethodId, Maped);
            if (Order == null) return BadRequest(new ApiRespones(400,"There is A Problem With Your Order "));
            return Ok(Order);
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(IReadOnlyList<OrderToReturnDto>),statusCode:200)]
        [ProducesResponseType(typeof(ApiRespones),statusCode:404)]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var Email=User.FindFirstValue(ClaimTypes.Email);
            var Orders = await _orderService.GetOrdersForSpecificUserAsync(Email);
            if (Orders == null) return  NotFound(new ApiRespones(404,"There is no order for this User"));
            var MappedOrders =  _mapper.Map<IReadOnlyList<Order>,IReadOnlyList <OrderToReturnDto>>(Orders);
            return Ok(MappedOrders);

        }
        [HttpGet("{Id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(OrderToReturnDto), statusCode: 200)]
        [ProducesResponseType(typeof(ApiRespones), statusCode: 404)]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(int Id)
        {
            var BuyerEmail=User.FindFirstValue(ClaimTypes.Email);
            var Order = await _orderService.GetOrderByIdFoeSpecificUserAsync(BuyerEmail, Id);
            if (Order == null) return NotFound(new ApiRespones(404));
            var MappedOrder=_mapper.Map<Order, OrderToReturnDto>(Order);
            return Ok(MappedOrder);
        }
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetAllDelivery()
        {
            var Delivery=await _unitOfWork.repo<DeliveryMethod>().GetAllAsync();
            return Ok(Delivery);
        }
    }
}
