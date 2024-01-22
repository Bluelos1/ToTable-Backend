using Microsoft.AspNetCore.Mvc;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Controllers;

   [Route("api/[controller]")]
   [ApiController]
public class RestaurantController:ControllerBase
{
    private readonly IRestaurantService _RestaurantService;
    
            public RestaurantController(IRestaurantService RestaurantService)
            {
                _RestaurantService = RestaurantService;
            }
    
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurantObject()
            { 
                var RestaurantObject = _RestaurantService.GetRestaurantObject();
              if (RestaurantObject== null)
              {
                  return NotFound();
              }
                return await RestaurantObject;
            }
    
            [HttpGet("{id}")]
            public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
            {
                var item = await _RestaurantService.GetRestaurant(id);
              if (item == null)
              {
                  return NotFound();
              }
                return item;
            }
    
            
            [HttpPut("{id}")]
            public async Task<IActionResult> PutRestaurant(int id, Restaurant Restaurant)
            {
                if (id != Restaurant.RestaurantId)
                {
                    return BadRequest();
                }
    
                await _RestaurantService.PutRestaurant(id, Restaurant);
                return NoContent();
            }
    
            
            [HttpPost]
            public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant Restaurant)
            {
                await _RestaurantService.PostRestaurant(Restaurant);
                return CreatedAtAction("GetRestaurant", new { id = Restaurant.RestaurantId }, Restaurant);
            }
    
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteRestaurant(int id)
            {
                if (_RestaurantService.GetRestaurant(id)== null)
                {
                    return NotFound();
                }
    
                await _RestaurantService.DeleteRestaurant(id);
                return NoContent();
            }


            [HttpGet("login/{login}/{password}")]
            public async Task<ActionResult<Restaurant>> GetRestaurantByCredentials(string login, string password)
            {
                var restaurant = await _RestaurantService.GetRestaurantByCredentials(login, password);
                if (restaurant == null)
                {
                    return NotFound();
                }
                return restaurant;
            }
}