using estore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace estore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly DataContex _context;
        public ProductController( DataContex contex)
        {
            _context = contex;
        }
        public IActionResult Index()
        {
            var productlist = _context.tblProducts.OrderBy(p => p.Id).ToList();
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
            var mn = _context.tblProducts.Find(id);
            if (mn == null)
                return NotFound();
            _context.tblProducts.Remove(mn);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            var pdList = (from m in _context.tblProducts
                          select new SelectListItem()
                          {
                              Text = m.CategoriId.ToString(),
                              Value = m.Id.ToString(),
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
        public IActionResult Create(TblProducts pd)
        {


            if (ModelState.IsValid)
            {
                //if (pd.ImageFile != null)
                //{
                //    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(pd.ImageFile.FileName);
                //    string filePath = Path.Combine("wwwroot/uploads", fileName);

                //    using (var stream = new FileStream(filePath, FileMode.Create))
                //    {
                //        await pd.ImageFile.CopyToAsync(stream);
                //    }

                //    pd.Images = "/uploads/" + fileName;
                //}


                _context.tblProducts.Add(pd);
               _context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(pd);

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
                              Value = m.Id.ToString(),
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
        public IActionResult Edit(TblProducts pd)
        {
            
            
                //if (pd.ImageFile != null)
                //{
                //    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(pd.ImageFile.FileName);
                //    string filePath = Path.Combine("wwwroot/uploads", fileName);

                //    using (var stream = new FileStream(filePath, FileMode.Create))
                //    {
                //        await pd.ImageFile.CopyToAsync(stream);
                //    }

                //    pd.Images = "/uploads/" + fileName;
                //}
            
            
            

            //_context.tblProducts.Update(pd);
            //await _context.SaveChangesAsync();
            //return RedirectToAction("Index");
            _context.tblProducts.Update(pd);
             _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
