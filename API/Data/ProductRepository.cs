using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Product.FindAsync(id);
        }

        public IEnumerable<Product> GetProductByName(string name)
        {
            return _context.Product.AsEnumerable().Select(x =>
                new Product
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Quantity = x.Quantity
                }
            )
            .Where(w => w.Name == name)
            .GroupBy(g => new { g.Name, g.Description })
            .Select(p => new Product
            {
                Id = p.Count(),
                Name = p.Key.Name,
                Description = p.Key.Description,
                Quantity = p.Sum(t => t.Quantity)
            });
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Product.AsEnumerable().Select(x =>
                new Product
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Quantity = x.Quantity
                }
            )
            .GroupBy(g => new { g.Name, g.Description })
            .Select(p => new Product
            {
                Id = p.Count(),
                Name = p.Key.Name,
                Description = p.Key.Description,
                Quantity = p.Sum(t => t.Quantity)
            });
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }

        public void Create(Product product)
        {
            _context.Entry(product).State = EntityState.Added;
        }
    }
}
