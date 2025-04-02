using estore.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace estore.Areas.Admin.Controllers;

    [Area("Admin")]
    public class HomeController : Controller
    {
       

        public IActionResult Index()
        {
            if (!Functions.IsLogin())
                return RedirectToAction("Index", "Login");
            return View();
        }
<<<<<<< HEAD

        public IActionResult Logout()
        {
            Functions._UserID = 0;
            Functions._UserName = string.Empty;
            Functions._Email = string.Empty;
            Functions._Message = string.Empty;
            return RedirectToAction("Index", "Home");
        }
    }

=======
    }
>>>>>>> 8f89f633277344c688e9ace82aab077c686073b2
