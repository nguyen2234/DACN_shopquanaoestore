using estore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace estore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly DataContex _context;
        public ProductController(DataContex contex)
        {
            _context = contex;
        }
        public IActionResult Index()
        {
            var productlist = _context.tblProducts
              .Include(p => p.Categori)           // Load danh mục liên quan
              .OrderBy(p => p.CategoriId)         // Sắp xếp theo danh mục
              .ToList();

            return View(productlist);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var mn = _context.tblProducts.Find(id);
            if (mn == null)
                return NotFound();
            return View(mn);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var mn = _context.tblProducts.Include(o => o.ProductDetails).FirstOrDefault(p => p.ProductId == id);
            if (mn == null)
                return NotFound();
            var details = _context.TblProductDetails.Where(o => o.ProductId == id).ToList();
            _context.TblProductDetails.RemoveRange(details); //removerange xóa nhieudong

            _context.tblProducts.Remove(mn);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            ViewBag.CategoryList = new SelectList(_context.categoris, "CategoriId", "Name");
            ViewBag.SizeList = _context.tblProductSizes.ToList();
            var pdList = (from m in _context.tblProducts
                          select new SelectListItem()
                          {
                              Text = m.CategoriId.ToString(),
                              Value = m.ProductId.ToString(),
                          }).ToList();
            pdList.Insert(0, new SelectListItem()
            {
                Text = "----- Lựa chọn -----",
                Value = "0"
            });
            ViewBag.pdList = pdList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Products pd, int selectedSizeId, int quantity)
        {
            // Thêm sản phẩm
            _context.tblProducts.Add(pd);
            _context.SaveChanges();

            // Nếu có chọn size và số lượng > 0 thì thêm ProductDetails
            if (selectedSizeId > 0 && quantity > 0)
            {
                var detail = new ProductDetails
                {
                    ProductId = pd.ProductId,
                    SizeId = selectedSizeId,
                    Quantity = quantity
                };
                _context.TblProductDetails.Add(detail);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var mn = _context.tblProducts.Find(id);
            if (mn == null)
                return NotFound();
            var mnList = (from m in _context.tblProducts
                          select new SelectListItem()
                          {
                              Text = m.CategoriId.ToString(),
                              Value = m.ProductId.ToString(),
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "------Lựa chọn------",
                Value = string.Empty
            });
            ViewBag.mnList = mnList;
            return View(mn);

        }
        [HttpPost]
        public IActionResult Edit(Products pd)
        {
            var item = _context.tblProducts.Include(o => o.ProductDetails).FirstOrDefault(p=>p.ProductId == pd.ProductId);
            if (item == null)
                return NotFound();
            var details = _context.TblProductDetails.Where(o => o.ProductId == pd.ProductId).ToList();
            _context.TblProductDetails.UpdateRange(details);
            _context.tblProducts.Update(pd);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
