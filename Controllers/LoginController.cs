using Microsoft.AspNetCore.Mvc;
using estore.Models;
using estore.Utilities;
using estore.Areas.Admin.Models;

namespace estore.Controllers
{
    public class LoginController : Controller
    {
        private readonly DataContex _context;

        public LoginController(DataContex context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Index(User user)
        {
            if (user == null) return NotFound();

            string pw = Functions.MD5Password(user.Password);
            var check = _context.users.Where(u => (u.UserName == user.UserName) && (u.Password == pw)).FirstOrDefault();
            if (check == null)
            {
                Functions._message = "Tên đăng nhập hoặc mật khẩu không đúng!";
                return View();
            }
            Functions._message = string.Empty;
            Functions._userid = check.UserID;
            Functions._username = string.IsNullOrEmpty(check.UserName) ? string.Empty : check.UserName;
            Functions._email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;
            return RedirectToAction("Index", "Home");
        }

    }
}
