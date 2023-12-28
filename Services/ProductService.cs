using Microsoft.EntityFrameworkCore;
using ToTable.Controllers;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Services;


public class ProductService : IProductService
{
    private readonly ToTableDbContext _context;
    private readonly ILogger<PaymentService> _logger;

    public ProductService(ToTableDbContext context, ILogger<PaymentService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task<List<Product>> GetProductItems()
    {
        try
        {
            return _context.ProductItems.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"NotFound");
            throw;
        }
        
    }

    public Task<Product> GetProduct(int id)
    {
        return _context.ProductItems.FirstOrDefaultAsync(x => x.ProductId == id);
    }

    public async Task PostProduct(Product Product)
    {
        _context.ProductItems.Add(Product);
        await _context.SaveChangesAsync();
    }

    public async Task PutProduct(int id, Product Product)
    {
        _context.Entry(Product).State = EntityState.Modified;
        await _context.SaveChangesAsync();    }

    public async Task DeleteProduct(int id)
    {
        var item = await _context.ProductItems.FindAsync(id);
        if (item != null)
        {
            _context.ProductItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public Task<bool> ProductExists(int id)
    {
        return _context.ProductItems.AnyAsync(x => x.ProductId == id);
    }       
}
