using Microsoft.EntityFrameworkCore;
using ToTable.Contract;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Services;

public class RestaurantService : IRestaurantService
{
    private readonly ToTableDbContext _context;
    private readonly ILogger<RestaurantService> _logger;

    public RestaurantService(ToTableDbContext context, ILogger<RestaurantService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public Task<List<Restaurant>> GetRestaurantObject()
    {
        try
        {
            return _context.RestaurantObject.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e,"NotFound");
            throw;
        }
        
    }

    public Task<Restaurant> GetRestaurant(int id)
    {
        return _context.RestaurantObject.FirstOrDefaultAsync(x => x.RestaurantId == id);
    }

    public async Task PostRestaurant(RestaurantDto restaurant)
    {
        var restaurantItem = new Restaurant
        {
            RestaurantName = restaurant.RestaurantName,
            Login = restaurant.Login,
            Password = restaurant.Password,
            TableQuantity = restaurant.TableQuantity,
            WaiterQantity = restaurant.WaiterQantity,
        };
        
        _context.RestaurantObject.Add(restaurantItem);
        await _context.SaveChangesAsync();
    }

    public async Task PutRestaurant(int id, RestaurantDto restaurant)
    {
        var restaurantItem = await _context.RestaurantObject.FirstOrDefaultAsync(x => x.RestaurantId == id);
        restaurantItem.RestaurantName = restaurant.RestaurantName;
        restaurantItem.Login = restaurant.Login;
        restaurantItem.Password = restaurant.Password;
        restaurantItem.TableQuantity = restaurant.TableQuantity;
        restaurantItem.WaiterQantity = restaurant.WaiterQantity;
        await _context.SaveChangesAsync();    
    }

    public async Task DeleteRestaurant(int id)
    {
        var item = await _context.RestaurantObject.FindAsync(id);
        if (item != null)
        {
            _context.RestaurantObject.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<Restaurant> GetRestaurantByCredentials(string login, string password)
    {
        return await _context.RestaurantObject.FirstOrDefaultAsync(r => r.Login == login && r.Password == password);
    }

    public Task<bool> RestaurantExists(int id)
    {
        return _context.RestaurantObject.AnyAsync(x => x.RestaurantId == id);
    }       
}