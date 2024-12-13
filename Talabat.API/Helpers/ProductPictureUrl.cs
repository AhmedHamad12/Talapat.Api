using AutoMapper;
using talabat.Modeles.Models;
using Talabat.API.DTO;

namespace Talabat.API.Helpers
{
    public class ProductPictureUrl : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrl(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.pictureUrl)) 
                return $"{_configuration["ApiBaseUrl"]}{source.pictureUrl}";
            return string.Empty ;
        }
    }
}
