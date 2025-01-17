using NayelPro.Data;
using NayelPro.Models;

namespace NayelPro.Services
{
    public class ProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var Categories = await _unitOfWork.Categories.GetAllCategoriesAsync();
            var Products = await _unitOfWork.Products.GetAllProductsAsync();

            var Res = (from p in Products
                      join c in Categories on p.CategoryId equals c.Id
                      select new Product
                      {
                          Id = p.Id,
                          Name = p.Name,
                          Description = p.Description,
                          CategoryName = c.Name,
                          CategoryId = c.Id,
                      }).ToList();
            return Res;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _unitOfWork.Products.GetProductByIdAsync(id);
        }

        public async Task<string> AddProductAsync(Product Product)
        {
            try
            {
                var Res = string.Empty;
                await _unitOfWork.Products.AddProductAsync(Product);
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

        public async Task<string> UpdateProductAsync(Product Product)
        {
            try
            {
                var Res = string.Empty;
                await _unitOfWork.Products.UpdateProductAsync(Product);
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

        public async Task<string> DeleteProductAsync(int id)
        {
            try
            {
                var Res = string.Empty;
                await _unitOfWork.Products.DeleteProductAsync(id);
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
