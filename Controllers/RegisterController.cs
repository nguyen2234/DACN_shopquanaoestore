using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using estore.Models;
using estore.Utilities;
using Microsoft.EntityFrameworkCore;
using estore.Areas.Admin.Models;


namespace estore.Controllers
{

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

        public ActionResult Index(User auser)
        {
            if (auser == null) return NotFound();
            var check = _context.users.Where(u => (u.UserName == auser.UserName)).FirstOrDefault();
            if (check != null)
            {
                Functions._Message = "Username already exists";
                return RedirectToAction("Index", "Register");
            }

            Functions._Message = string.Empty;
            auser.Password = Functions.MD5Password(auser.Password);
            _context.users.Add(auser);
            _context.SaveChanges();
            return RedirectToAction("Index", "Login");
        }
    }
}
