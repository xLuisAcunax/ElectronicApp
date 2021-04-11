using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var users = await _context.Product.ToListAsync();

            return users;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _context.Product.FindAsync(id);
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

            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return new ProductDto
            {
                Name = product.Name,
                Description = product.Description,
                Quantity = product.Quantity
            };
        }
    }
}
