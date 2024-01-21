using Microsoft.EntityFrameworkCore;
using ToTable.Contract;
using ToTable.Controllers;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Services;


public class ProductService : IProductService
{
    private readonly ToTableDbContext _context;
    private readonly ILogger<ProductService> _logger;

    public ProductService(ToTableDbContext context, ILogger<ProductService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task<List<Product>> GetProductObject()
    {
        try
        {
            return _context.ProductObject.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"NotFound");
            throw;
        }
        
    }

    public Task<Product> GetProduct(int id)
    {
        return _context.ProductObject.FirstOrDefaultAsync(x => x.ProductId == id);
    }

    public async Task PostProduct(ProductDto product)
    {

        var productItem = new Product
        {
            ProductName = product.ProductName,
            ProductDescription = product.ProductDescription,
            ProductPrice = product.ProductPrice,
            ProductStatus = product.ProductStatus,
            ImageUrl = product.ImageUrl,
            RestaurantId = product.RestaurantId,
        };
        _context.ProductObject.Add(productItem);
        await _context.SaveChangesAsync();
    }

    public async Task PutProduct(int id, ProductDto Product)
    {
        _context.Entry(Product).State = EntityState.Modified;
        await _context.SaveChangesAsync();    }

    public async Task DeleteProduct(int id)
    {
        var item = await _context.ProductObject.FindAsync(id);
        if (item != null)
        {
            _context.ProductObject.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public Task<bool> ProductExists(int id)
    {
        return _context.ProductObject.AnyAsync(x => x.ProductId == id);
    }       
}
