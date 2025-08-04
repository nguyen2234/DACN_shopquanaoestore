using estore.Areas.Admin.Controllers;
using estore.Models;
using Microsoft.AspNetCore.Mvc;
using estore.Utilities;

namespace estore.Controllers
{
    public class ContactController : Controller
    {
        private readonly DataContex _context;
        public ContactController(DataContex context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!Functions.islogin())
            {
                return RedirectToAction("Index", "Login");
            }

            var contact = new Contact
            {
                userid = Functions._userid,
                Name = Functions._username,
                Email = Functions._email
            };

            return View(contact);
        }

        [HttpPost]
        public IActionResult AddContact(Contact ct)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", ct);
            }

            ct.userid = Functions._userid;
            ct.SentAt = DateTime.Now;
            ct.IsActive = false;
            _context.contacts.Add(ct);
            _context.SaveChanges();

            ViewBag.Success = "Gửi yêu cầu thành công!";
            ModelState.Clear();

            // Trả lại form trống với thông tin người dùng
            var contact = new Contact
            {
                userid = Functions._userid,
                Name = Functions._username,
                Email = Functions._email
            };

            return View("Index", contact); 

        }
    }
}
