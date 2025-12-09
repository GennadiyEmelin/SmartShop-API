using Microsoft.EntityFrameworkCore;
using TestASP.Contracts;
using TestASP.Models;

namespace TestASP.Services
{
    public class ProductService
    {
        private readonly AppDbContext _db;
        public ProductService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Product> AddProductAsync(string name, decimal price, int categoryId)
        {
            var product = new Product(name, price, categoryId);
            try
            {
                await _db.Products.AddAsync(product);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Ошибка добавления в БД {ex.Message}");
            }
            return product;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _db.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.Id == id); 
        }

        public async Task<Product?> UpdateProductAsync(int id, string name, decimal price, int categoryId)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return null;
            }
            product.Name = name;
            product.Price = price;
            product.CategoryId = categoryId;
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProductAsync(Product product)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Product> Query()
        {
            return _db.Products.AsQueryable();
        }
    }
}
