
using ToTable.Models;

namespace ToTable.Interfaces;


public interface IProductService
{
    Task<List<Product>> GetProductItems();
    Task<Product> GetProduct(int id);
    Task PostProduct(Product Product);
    Task PutProduct(int id, Product Product);
    Task DeleteProduct(int id);
}