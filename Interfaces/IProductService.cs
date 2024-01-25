using System.Collections.Generic;
using System.Threading.Tasks;
using ToTable.Contract;
using ToTable.Models;

namespace ToTable.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProductObject();
        Task<Product> GetProduct(int id);
        Task PostProduct(ProductDto product);
        Task PutProduct(int id, ProductDto product);
        Task DeleteProduct(int id);
        Task<IEnumerable<ProductDto>> GetProductsByRestaurantId(int restaurantId);
    }
}
