using Microsoft.AspNetCore.Mvc;
using NayelPro.Models;
using NayelPro.Services;

namespace NayelPro.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoriesService _categoryService;

        public CategoryController(CategoriesService categoriesService)
        {
            _categoryService = categoriesService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var Categorys = await _categoryService.GetAllCategoriesAsync();
            return Json(Categorys);
        }

        [HttpPost]
        public async Task<JsonResult> Create(Category Category)
        {
            var Res = await _categoryService.AddCategoryAsync(Category);
            return Json(Res);
        }

        public async Task<JsonResult> GetById(int id)
        {
            var Category = await _categoryService.GetCategoryByIdAsync(id);
            return Json(Category);
        }

        [HttpPost]
        public async Task<JsonResult> Update(Category Category)
        {
            var Res = await _categoryService.UpdateCategoryAsync(Category);
            return Json(Res);
        }

        public async Task<JsonResult> Delete(int id)
        {
            var Category = await _categoryService.GetCategoryByIdAsync(id);
            if (Category != null && Category.Id > 0)
            {
                var Res = await _categoryService.DeleteCategoryAsync(id);
                return Json(Res);
            }
            return Json(string.Empty);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<JsonResult> DeleteConfirmed(int id)
        {
            var Res = await _categoryService.DeleteCategoryAsync(id);
            return Json(Res);
        }
    }
}
