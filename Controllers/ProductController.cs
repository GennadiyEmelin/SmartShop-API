using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using TestASP.DTO;
using TestASP.Models;
using TestASP.Services;

namespace TestASP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDTO productDto)
        {
            var product = await _productService.AddProductAsync(productDto.Name, productDto.Price);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpGet("GetAllProducts")]
        public async Task<List<Product>> GetAllProduct()
        {
            return await _productService.GetAllProductsAsync();
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Product>> GetById(int Id)
        { 
            var product = await _productService.GetProductByIdAsync(Id);
            if (product == null) { return NotFound(); }
            return product;
        }

        [HttpPut("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] AddProductDTO dto)
        {
            var updateProduct = await _productService.UpdateProductAsync(id, dto.Name, dto.Price);
            return NoContent();
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productService.DeleteProductAsync(product);
            return NoContent();
        }

        [HttpGet("Search")]
        public async Task<List<Product>> SearchProduct(
            [FromQuery] string? Name,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice)
        {
            var query = await _productService.GetAllProductsAsync();
            var products = query.AsQueryable(); 
            if (!string.IsNullOrEmpty(Name))
            {
                products = products.Where(p => p.Name.Contains(Name, StringComparison.OrdinalIgnoreCase));
            }
            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Price <= maxPrice.Value);
            }
            return products.ToList();

        }

        [HttpGet("SortedProduct")]
        public async Task<IActionResult> GetSortedProductAsync(
            [FromQuery] string? sortedBy,
            [FromQuery] bool Descending)
        {
            IQueryable<Product> query = _productService.Query();

            if (!string.IsNullOrEmpty(sortedBy))
            {
                query = sortedBy.ToLower() switch
                {
                    "name" => Descending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                    "price" => Descending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                    _ => query
                };
            }
           var result = query.ToList();
            return Ok(result);
        }
    }
}
