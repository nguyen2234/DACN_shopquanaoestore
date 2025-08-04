using estore.Models;
using estore.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace estore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly DataContex _context;
        public UserController(DataContex context)
        { _context = context; }
        public IActionResult Index()
        {
            var mnList = _context.users.OrderBy(m => m.UserID).ToList();
            return View(mnList);
        }        
    }
}
