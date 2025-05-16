using estore.Models;
using estore.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace estore.Controllers
{

    public class ProductController : Controller
    {
        private readonly DataContex _context;
        public ProductController(DataContex context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var pd = _context.tblProducts.OrderBy(m => m.IsActive).ToList();
            return View(pd);
        }
        [Route("Product/details/{id}")]
        public IActionResult Details(int id)
        {
            var product = _context.tblProducts
            .Include(p => p.Categori)
            .Include(p => p.ProductDetails)
            .ThenInclude(d => d.ProductSize)
            .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
                return NotFound();

            return View(product);

        }
        [HttpPost]
        public IActionResult Order(int ProductId, int SizeId, int Quantity)
        {
            var product = _context.tblProducts.Find(ProductId);
            var size = _context.tblProductSizes.Find(SizeId);
            if (product == null || size == null)
            {
                TempData["Error"] = " Sản phẩm hoặc size không tồn tại";
                return RedirectToAction("Details", new { id = product });
            }
                TempData["Message"] = $"Đặt hàng thành công sản phẩm ID {ProductId}, size {SizeId}, SL {Quantity}";
            return RedirectToAction("Detail", new { id = ProductId });
        }

    }
}
