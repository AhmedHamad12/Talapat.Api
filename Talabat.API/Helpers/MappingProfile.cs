using AutoMapper;
using talabat.Core.Models;
using talabat.Core.Models.Identity;
using talabat.Core.Models.Order_Aggreate;
using talabat.Modeles.Models;
using Talabat.API.DTO;
using IdentityAddress = talabat.Core.Models.Identity.Address;
using OrderAddress = talabat.Core.Models.Order_Aggreate.Address;

namespace Talabat.API.Helpers
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
              .ForMember(d => d.productType, o => o.MapFrom(s => s.productType.name))
              .ForMember(d=>d.productBrand,o=>o.MapFrom(s=>s.productBrand.name))
              .ForMember(d=>d.pictureUrl,o=>o.MapFrom<ProductPictureUrl>());
            CreateMap<IdentityAddress, AddressDto>().ReverseMap();
            CreateMap<AddressDto, OrderAddress>();
            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDto,BasketItem>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d=>d.deliveryMethod,o=>o.MapFrom(s=>s.DeliveryMethod.ShortName))
                .ForMember(d=>d.deliveryMethodCost,o=>o.MapFrom(s=>s.DeliveryMethod.Cost))
                ;
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.productId, o => o.MapFrom(d => d.Product.ProductId))
                .ForMember(d=>d.productName,o=>o.MapFrom(d=>d.Product.ProductName))
                .ForMember(d=>d.pictureUrl,o=>o.MapFrom<OrderPictureUrl>());

                
        }
    }
}
