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
            var productList = _context.tblProducts
                .Include(p => p.Categori)
                .Select(p => new ProductIndexViewModel
                { Images = p.Images,
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price,
                    Categori = p.Categori.Name,
                    IsActive = p.IsActive,
                    TotalQuantity = _context.TblProductDetails
                                        .Where(d => d.ProductId == p.ProductId)
                                        .Sum(d => (int?)d.Quantity) ?? 0
                })
                .ToList();

            return View(productList);
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
            _context.TblProductDetails.RemoveRange(details);

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
        public IActionResult Create(Products pd, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/product", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                pd.Images = "/uploads/product/" + fileName; 
            }

            _context.tblProducts.Add(pd);
            _context.SaveChanges();

            var sizeList = _context.tblProductSizes.ToList();
            foreach (var size in sizeList)
            {
                var detail = new ProductDetails
                {
                    ProductId = pd.ProductId,
                    SizeId = size.SizeId,
                    Quantity = 0
                };
                _context.TblProductDetails.Add(detail);
            }
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

       
        public IActionResult Edit(int id)
        {
            var product = _context.tblProducts.Find(id);
            if (product == null) return NotFound();

            var vm = new ProductEditViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Title = product.Title,
                Contents = product.Contents,
                Price = product.Price,
                CategoriId = product.CategoriId,
                IsActive = product.IsActive,
                Images = product.Images
            };

            ViewBag.mnList = new SelectList(_context.categoris.ToList(), "CategoriId", "Name", product.CategoriId);
            return View(vm);
        }


        [HttpPost]
        public IActionResult Edit(ProductEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.mnList = new SelectList(_context.categoris.ToList(), "CategoriId", "Name", model.CategoriId);
                return View(model);
            }

            var product = _context.tblProducts.Find(model.ProductId);
            if (product == null) return NotFound();

            product.Name = model.Name;
            product.Title = model.Title;
            product.Contents = model.Contents;
            product.Price = model.Price;
            product.CategoriId = model.CategoriId;
            product.IsActive = model.IsActive;
            product.Images = model.Images;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }     

        public IActionResult UpdateQuantity(int id)
        {
            var product = _context.tblProducts.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();

            var details = _context.TblProductDetails
                .Include(p => p.ProductSize)
                .Where(p => p.ProductId == id)
                .ToList();

            var vm = new UpdateQuantityViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.Name,
                Items = details.Select(d => new UpdateQuantityViewModel.QuantityItem
                {
                    SizeId = d.SizeId,
                    SizeName = d.ProductSize.Size,
                    Quantity = d.Quantity
                }).ToList()
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult UpdateQuantity(UpdateQuantityViewModel vm)
        {
            foreach (var item in vm.Items)
            {
                var detail = _context.TblProductDetails
                    .FirstOrDefault(p => p.ProductId == vm.ProductId && p.SizeId == item.SizeId);
                if (detail != null)
                {
                    detail.Quantity = item.Quantity;
                }
            }

            _context.SaveChanges();
            TempData["Message"] = "Cập nhật số lượng thành công!";
            return RedirectToAction("Index");
        }







    }
}
