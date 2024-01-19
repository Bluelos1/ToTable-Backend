using System.Collections.Generic;
using System.Threading.Tasks;
using ToTable.Models;

namespace ToTable.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProductItems();
        Task<Product> GetProduct(int id);
        Task PostProduct(Product product);
        Task PutProduct(int id, Product product);
        Task DeleteProduct(int id);
    }
}
