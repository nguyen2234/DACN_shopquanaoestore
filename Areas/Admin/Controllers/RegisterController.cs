using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using estore.Areas.Admin.Models;
using estore.Models;
using estore.Utilities;
using Microsoft.EntityFrameworkCore;


namespace estore.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class RegisterController : Controller
    {
        private readonly DataContex _context;
        public RegisterController(DataContex context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Index(AdminUser auser)
        {
            if (auser == null) return NotFound();
            var check = _context.AdminUsers.Where(u => (u.UserName == auser.UserName)).FirstOrDefault();
            if (check != null)
            {
                Functions._Message = "Username already exists";
                return RedirectToAction("Index", "Register");
            }

            Functions._Message = string.Empty;
            auser.Password = Functions.MD5Password(auser.Password);
            _context.AdminUsers.Add(auser);
            _context.SaveChanges();
            return RedirectToAction("Index", "Login");
        }
    }
}
