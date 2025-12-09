using TestASP.Contracts;

namespace TestASP.Services
{
    public class CategoryService
    {
        private readonly AppDbContext _db;
        public CategoryService(AppDbContext db) 
        {
            _db = db;
        }

        public async Task AddCategoryAsync(string name)
        {
            var category = new Models.Category(name);
            try
            {
                await _db.Categories.AddAsync(category);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Ошибка добавления в БД {ex.Message}");
            }
        }
    }
}
