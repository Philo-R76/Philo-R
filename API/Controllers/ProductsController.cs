using Core.Entites;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using API.DTos;
using AutoMapper;
using API.Helpers;
using API.Errors;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductsController : BaseApiController
    {

        
        private readonly IGenericRepository<Product> productsRepo;
        private readonly IGenericRepository<ProductBrand> productBrandRepo;
        private readonly IGenericRepository<ProductType> productTypeRepo;
        private readonly IMapper mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductBrand> productBrandRepo,IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {
           this.mapper = mapper;
           this.productTypeRepo = productTypeRepo;
           this.productBrandRepo = productBrandRepo;
           this.productsRepo = productsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<Product>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
           var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
           var countSpec = new ProductWithFiltersForCountSpecification(productParams);
           var totalItems = await this.productsRepo.CountAsync(countSpec);
           var products = await this.productsRepo.ListAsync(spec);
           var data = this.mapper
           .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
           return Ok( new Pagination<ProductToReturnDto>(productParams.PageIndex,productParams.PageSize, totalItems, data));
        }
        
        [HttpGet("{id}")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>>GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await this.productsRepo.GetEntityWithSepc(spec);

            if(product == null) return NotFound(new ApiResponse(404));

            return this.mapper.Map<Product, ProductToReturnDto>(product);
            
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>>GetBrands()
        {
            return Ok(await this.productBrandRepo.ListAllAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>>GetTypes()
        {
            return Ok(await this.productTypeRepo.ListAllAsync());
        }
    }
}
