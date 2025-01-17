using NayelPro.Data;
using NayelPro.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NayelPro.Services
{
    public class CategoriesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _unitOfWork.Categories.GetAllCategoriesAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _unitOfWork.Categories.GetCategoryByIdAsync(id);
        }

        public async Task<string> AddCategoryAsync(Category Category)
        {
            try
            {
                var Res = string.Empty;
                await _unitOfWork.Categories.AddCategoryAsync(Category);
                var Added = await _unitOfWork.CompleteAsync().ConfigureAwait(false);
                if (Added == 1)
                {
                    Res = "Added";
                }
                return Res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public async Task<string> UpdateCategoryAsync(Category Category)
        {
            try
            {
                var Res = string.Empty;
                await _unitOfWork.Categories.UpdateCategoryAsync(Category);
                var Updated = await _unitOfWork.CompleteAsync().ConfigureAwait(false);
                if (Updated == 1)
                {
                    Res = "Updated";
                }
                return Res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DeleteCategoryAsync(int id)
        {
            try
            {
                var Res = string.Empty;
                await _unitOfWork.Categories.DeleteCategoryAsync(id);
                var Deleted = await _unitOfWork.CompleteAsync().ConfigureAwait(false);
                if (Deleted == 1)
                {
                    Res = "Deleted";
                }
                return Res;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
    }
}
