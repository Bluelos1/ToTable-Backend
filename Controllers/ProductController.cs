using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> PutProduct(int id, ProductDto product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            await _productService.PutProduct(id, product);
            return NoContent();
        }

        
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductDto product)
        {
            await _productService.PostProduct(product);
            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_productService.GetProduct(id)== null)
            {
                return NotFound();
            }

            await _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
