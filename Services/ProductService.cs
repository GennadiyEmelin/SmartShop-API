using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<Product> AddProductAsync(string name, decimal price)
        {
            var product = new Product(name, price);
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
            return await _db.Products.ToListAsync<Product>();  
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.Id == id); 
        }

        public async Task<Product?> UpdateProductAsync(int id, string name, decimal price)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return null;
            }
            product.Name = name;
            product.Price = price;
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
