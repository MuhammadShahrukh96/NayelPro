using Microsoft.AspNetCore.Mvc;
using NayelPro.Models;
using NayelPro.Services;

namespace NayelPro.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync();
            return Json(products);
        }

        [HttpPost]
        public async Task<JsonResult> Create(Product product)
        {
            var Res = await _productService.AddProductAsync(product);
            return Json(Res);
        }

        public async Task<JsonResult> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Json(product);
        }

        [HttpPost]
        public async Task<JsonResult> Update(Product product)
        {
            var Res = await _productService.UpdateProductAsync(product);
            return Json(Res);
        }

        public async Task<JsonResult> Delete(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product != null && product.Id > 0)
            {
                var Res = await _productService.DeleteProductAsync(id);
                return Json(Res);
            }
            return Json(string.Empty);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<JsonResult> DeleteConfirmed(int id)
        {
            var Res = await _productService.DeleteProductAsync(id);
            return Json(Res);
        }
    }

}
