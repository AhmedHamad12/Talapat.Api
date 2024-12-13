using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using talabat.Core;
using talabat.Core.Specificatios;
using talabat.Modeles.Models;
using talabat.Modeles.Reposituory;
using Talabat.API.DTO;
using Talabat.API.Errors;
using Talabat.API.Helpers;

namespace Talabat.API.Controllers
{
   
    public class ProductsController : APIBaseController
    {
     
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ProductsController(IUnitOfWork unitOfWork,IMapper mapper )
        {
           
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }
      //  [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [CashedAttribute(300)]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProductsAsync([FromQuery]productSpecParams Params) 
        {
            var spec=new ProductWithProductBrandAndTypeSpec(Params);
            var products= await _unitOfWork.repo<Product>().GetAllWithSpecAsync(spec);
            var mapedProduct = _mapper.Map<IReadOnlyList <Product>,IReadOnlyList< ProductToReturnDto>>(products);
            var CountSpec = new ProductFilterationToGetCountAsync(Params);
            var Count=await _unitOfWork.repo<Product>().GetCountBySpecAsync(CountSpec);
           // OkObjectResult result=new OkObjectResult(products);
           // return result;
           //we can use this 
           return Ok(new Pagination<ProductToReturnDto>(Params.pageIndex,Params.PageSize, mapedProduct,Count));
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(ProductToReturnDto),200)]
        [ProducesResponseType(typeof(ApiRespones),404)]
        public async Task<ActionResult<Product>> GetProductAsync(int Id)
        {
            var spec= new ProductWithProductBrandAndTypeSpec(Id);
            var product=await _unitOfWork.repo<Product>().GetByIdWithSpecificationAsync(spec);
            if (product == null) return NotFound(new ApiRespones(404)); 
            var mapedProduct=_mapper.Map<Product,ProductToReturnDto>(product);
            return Ok(mapedProduct);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var brands= await _unitOfWork.repo<ProductBrand>().GetAllAsync();
            return Ok(brands);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllTypes()
        {
            var types = await _unitOfWork.repo<ProductType>().GetAllAsync();
            return Ok(types);
        }
    }
}
