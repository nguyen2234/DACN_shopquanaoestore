using estore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace estore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly DataContex _context;

        public ContactController(DataContex context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var contacts = _context.contacts
                .OrderByDescending(c => c.SentAt)
                .ToList();

            return View(contacts);
        }

        public IActionResult Details(int id)
        {
            var contact = _context.contacts.FirstOrDefault(c => c.Id == id);
            if (contact == null) return NotFound();
            return View(contact);
        }
        [HttpPost]
        public IActionResult MarkAsRead(int id)
        {
            var contact = _context.contacts.Find(id);
            if (contact == null) return NotFound();
            contact.IsActive = true;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var contact = _context.contacts.Find(id);
            if (contact == null) return NotFound();

            _context.contacts.Remove(contact);
            _context.SaveChanges();
            TempData["Success"] = "Xoá thành công!";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var contact = _context.contacts.Find(id);
            if (contact == null) return NotFound();
            _context.contacts.Remove(contact);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
