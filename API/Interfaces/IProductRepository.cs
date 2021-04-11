using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IProductRepository
    {
        void Update(Product product);
        void Create(Product product);
        Task<bool> SaveAllAsync();
        IEnumerable<Product> GetProducts();
        Task<Product> GetProductByIdAsync(int id);
        IEnumerable<Product> GetProductByName(string name);
    }
}
