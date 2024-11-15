using Application;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts()
        {
            // Lekérjük az összes terméket a szolgáltatástól
            var products = await _productService.GetAllProductsAsync();

            // Mappoljuk a Product objektumokat ProductResponse típusúra
            var productResponses = products.Adapt<IEnumerable<ProductResponse>>();

            // Visszaadjuk a mappolt eredményt
            return Ok(productResponses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProductById(int id)
        {

            var prod = await _productService.GetProductByIdAsync(id);
            if (prod == null)
            {
                return NotFound();
            }
           
            // Mappoljuk a Category objektumot CategoryResponse típusra
            var map = prod.Adapt<ProductResponse>();

            // Visszaadjuk a mappolt CategoryResponse objektumot
            return Ok(map);

         
            
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(CreateProductRequest product)
        {

            var map = product.Adapt<Product>();
            await _productService.AddProductAsync(map);
            return CreatedAtAction(nameof(GetProductById), new { id = map.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, UpdateProductRequest product)
        {

            var map = product.Adapt<Product>();

            if (id != map.Id)
            {
                return BadRequest();
            }

            await _productService.UpdateProductAsync(map);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }

}
