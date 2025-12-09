using Microsoft.AspNetCore.Mvc;
using TestASP.DTO;
using TestASP.Services;

namespace TestASP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController: ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService) 
        {
            _categoryService = categoryService;
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryDTO dto)
        {
            await _categoryService.AddCategoryAsync(dto.Name);
            return Ok();
        }
    }
}
