using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToTable.Contract;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Controllers;

public class RestaurantController:ControllerBase
{
    private readonly IRestaurantService _restaurantService;
    
            public RestaurantController(IRestaurantService restaurantService)
            {
                _restaurantService = restaurantService;
            }
    
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurantObject()
            { 
                var RestaurantObject = _restaurantService.GetRestaurantObject();
              if (RestaurantObject== null)
              {
                  return NotFound();
              }
                return await RestaurantObject;
            }
    
            [HttpGet("{id}")]
            public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
            {
                var item = await _restaurantService.GetRestaurant(id);
              if (item == null)
              {
                  return NotFound();
              }
                return item;
            }
    
            
            [HttpPut("{id}")]
            public async Task<IActionResult> PutRestaurant(int id, RestaurantDto restaurant, IValidator<RestaurantDto> validator)
            {
                var validationResult = await validator.ValidateAsync(restaurant);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
                }

                if (id != restaurant.RestaurantId)
                {
                    return BadRequest();
                }

                try
                {
                    await _restaurantService.PutRestaurant(id, restaurant);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }

                return NoContent();
            }
    
            
            [HttpPost]
            public async Task<ActionResult<Restaurant>> PostRestaurant(RestaurantDto restaurant, IValidator<RestaurantDto> validator)
            {
                var validationResult = await validator.ValidateAsync(restaurant);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
                }

                try
                {
                    await _restaurantService.PostRestaurant(restaurant);

                    return CreatedAtAction("GetRestaurant", new { id = restaurant.RestaurantId }, restaurant);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, "An error occurred while creating the restaurant.");
                }
                
            }
    
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteRestaurant(int id)
            {
                var restaurant = await _restaurantService.GetRestaurant(id);
                if (restaurant == null)
                {
                    return NotFound();
                }
    
                await _restaurantService.DeleteRestaurant(id);
                return NoContent();
            }
}