using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToTable.Contract;
using ToTable.Interfaces;
using ToTable.Models;

namespace ToTable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductObject()
        { 
            var productObject = _productService.GetProductObject();
          if (productObject== null)
          {
              return NotFound();
          }
            return await productObject;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var item = await _productService.GetProduct(id);
          if (item == null)
          {
              return NotFound();
          }
            return item;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDto product, IValidator<ProductDto> validator)
        {
            var validationResult = await validator.ValidateAsync(product);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            if (id != product.ProductId)
            {
                return BadRequest("Mismatch between ID in URL and product ID.");
            }

            try
            {
                await _productService.PutProduct(id, product);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the product.");
            }

            return NoContent();
        }

        
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductDto product,IValidator<ProductDto> validator)
        {
            var validationResult = await validator.ValidateAsync(product);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            try
            {
                await _productService.PostProduct(product);
                return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the product.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var prod = await _productService.GetProduct(id);
            if (prod== null)
            {
                return NotFound();
            }

            await _productService.DeleteProduct(id);
            return NoContent();
        }

         [HttpGet("restaurant/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByRestaurantId(int restaurantId)
        {
            var products = await _productService.GetProductsByRestaurantId(restaurantId);
            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }


    }
}
