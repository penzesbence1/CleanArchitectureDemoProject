using Application;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetCategory(int id)
        {
            // Várjuk meg az aszinkron hívás befejezését
            var category = await _categoryService.GetCategoryByIdAsync(id);

            // Mappoljuk a Category objektumot CategoryResponse típusra
            var map = category.Adapt<CategoryResponse>();

            // Visszaadjuk a mappolt CategoryResponse objektumot
            return Ok(map);
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryResponse>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            // Mappolod a Category listát CategoryResponse listára az Adapt segítségével
            var categoryResponses = categories.Adapt<List<CategoryResponse>>();

            return Ok(categoryResponses);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryRequest categoryrequest)
        {

            var req = categoryrequest.Adapt<Category>();
            
            await _categoryService.AddCategoryAsync(req);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, UpdateCategoryRequest category)
        {

            var map = category.Adapt<Category>();

            if (id != map.Id)
            {
                return BadRequest();
            }
            await _categoryService.UpdateCategoryAsync(map);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }


        [HttpGet("{id}/product-count")]
        public async Task<ActionResult<CountDTO>> GetCategoryProductCount(int id)
        {
            try
            {
                var result = await _categoryService.GetCategoryProductCountAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
