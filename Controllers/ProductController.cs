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
        public IActionResult Index(int page = 1, int pageSize = 9, string? search = null, int? categoryId = null, int? minPrice = null, int? maxPrice = null)
        {
            var query = _context.tblProducts.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p =>
                    (p.Name != null && p.Name.Contains(search)) ||
                    (p.Description != null && p.Description.Contains(search))
                );
                ViewBag.CurrentSearch = search;
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoriId == categoryId.Value);
                ViewBag.CurrentCategoryId = categoryId;
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
                ViewBag.MinPrice = minPrice;
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
                ViewBag.MaxPrice = maxPrice;
            }

            ViewBag.Categories = _context.categoris.ToList();
            int totalProducts = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            var products = query
            .OrderBy(p => p.IsActive)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
            return View(products);

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
