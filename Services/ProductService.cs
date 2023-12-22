using Microsoft.EntityFrameworkCore;
using ToTable.Controllers;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Services;


public class ProductService : IProductService
{
    private readonly ToTableDbContext _dbContext;

    public ProductService(ToTableDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Product>> GetProductItems()
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProduct(int id)
    {
        throw new NotImplementedException();
    }

    public Task PostProduct(Product Product)
    {
        throw new NotImplementedException();
    }

    public Task PutProduct(int id, Product Product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }
}