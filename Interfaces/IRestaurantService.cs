using ToTable.Contract;
using ToTable.Models;

namespace ToTable.Interfaces;

public interface IRestaurantService
{
    Task<List<Restaurant>> GetRestaurantObject();
    Task<Restaurant> GetRestaurant(int id);
    Task PostRestaurant(RestaurantDto Restaurant);
    Task PutRestaurant(int id, RestaurantDto Restaurant);
    Task DeleteRestaurant(int id);
    Task<Restaurant> GetRestaurantByCredentials(string login, string password);
}