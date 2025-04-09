using estore.Models;
using estore.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace estore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlideController : Controller
    {
        private readonly DataContex _context;
        public SlideController(DataContex context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var mnList = _context.slides.OrderBy(m => m.Id).ToList();
            return View(mnList);
            
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.slides.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            return View(mn);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {           
            var deleMenu = _context.slides.Find(id);
            if (deleMenu == null)
            {
                return NotFound();
            }
            _context.slides.Remove(deleMenu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {       
            var mnList = (from m in _context.slides
                          select new SelectListItem()
                          {
                              Text = m.Title,
                              Value = m.Id.ToString(),
                          }).ToList();

            ViewBag.mnList = mnList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slide mn)
        {         
            if (ModelState.IsValid)
            {
                _context.slides.Add(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mn);
        }
        public IActionResult Edit(int? id)
        {           
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var mn = _context.slides.Find(id);
            if (mn == null)
            {
                return NotFound();
            }
            var mnList = (from m in _context.slides
                          select new SelectListItem()
                          {
                              Text = m.Title,
                              Value = m.Id.ToString(),
                          }).ToList();
            mnList.Insert(0, new SelectListItem()
            {
                Text = "---Select----",
                Value = string.Empty
            });
            ViewBag.mnList = mnList;
            return View(mn);
        }
        [HttpPost]
        public IActionResult Edit(Slide mn)
        {           
            if (ModelState.IsValid)
            {
                _context.slides.Update(mn);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mn);
        }
    }
}
