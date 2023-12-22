using Microsoft.EntityFrameworkCore;
using ToTable.Controllers;
using ToTable.Models;

namespace ToTable.Services;

public class ProductService : IProductService
{
    private readonly ToTableDbContext _dbContext;

    public ProductService(ToTableDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}