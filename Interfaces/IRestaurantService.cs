using ToTable.Models;

namespace ToTable.Interfaces;

public interface IRestaurantService
{
    Task<List<Restaurant>> GetRestaurantObject();
    Task<Restaurant> GetRestaurant(int id);
    Task PostRestaurant(Restaurant Restaurant);
    Task PutRestaurant(int id, Restaurant Restaurant);
    Task DeleteRestaurant(int id);
}