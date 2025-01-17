using NayelPro.Models;

namespace NayelPro.Data
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category product);
        Task UpdateCategoryAsync(Category product);
        Task DeleteCategoryAsync(int id);
    }
}
