using estore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Common;

namespace estore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        private readonly DataContex _context;
        public MenuController(DataContex context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var mnlist = _context.tblMenus.OrderBy(m => m.MenuId).ToList();
            return View(mnlist);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var mn = _context.tblMenus.Find(id);
            if (mn == null)

                return NotFound();
            return View(mn);

        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var delmn = _context.tblMenus.Find(id);
            if (delmn == null)
                return NotFound();
            _context.tblMenus.Remove(delmn);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            var mnlist = (from m in _context.tblMenus
                          select new SelectListItem()
                          {
                              Text = (m.Levels == 1) ? m.MenuName : "--" + m.MenuName,
                              Value = m.MenuId.ToString()
                          }).ToList();
            mnlist.Insert(0, new SelectListItem()
            {
                Text = "---Lựa chọn---",
                Value = "0"
            });
            ViewBag.mnlist = mnlist;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Menu mn)
        {
            if (ModelState.IsValid)
            {
                _context.tblMenus.Add(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var mn = _context.tblMenus.Find(id);
            if (mn == null) return NotFound();
            var mnlist = (from m in _context.tblMenus
                          select new SelectListItem()
                          {
                              Text = (m.Levels == 1) ? m.MenuName : "--" + m.MenuName,
                              Value = m.MenuId.ToString()
                          }).ToList();
            mnlist.Insert(0, new SelectListItem()
            {
                Text = "---- Lựa chọn ----",
                Value = "0"
            });
            ViewBag.mnlist = mnlist;
            return View(mn);
        }
        [HttpPost]
        public IActionResult Edit(Menu mn)
        {
            if (ModelState.IsValid)
            {
                _context.tblMenus.Update(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
