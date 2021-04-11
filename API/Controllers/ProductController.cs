using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<ProductDto>> GetProducts()
        {
            var products = _productRepository.GetProducts();

            var productsToReturn = _mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(productsToReturn);
        }

        [HttpGet("{name}")]
        public ActionResult<IEnumerable<ProductDto>> GetProducts(string name)
        {
            var products = _productRepository.GetProductByName(name);

            var productsToReturn = _mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(productsToReturn);
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<ProductDto>> Register(ProductCreateDto productCreateDto)
        {
            var product = new Product
            {
                Name = productCreateDto.Name.ToLower(),
                Description = productCreateDto.Description.ToLower(),
                Quantity = productCreateDto.Quantity
            };

            _productRepository.Create(product);
            await _productRepository.SaveAllAsync();

            return new ProductDto
            {
                Name = product.Name,
                Description = product.Description,
                Quantity = product.Quantity
            };
        }
    }
}
