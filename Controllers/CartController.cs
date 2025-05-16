using estore.Areas.Admin.Models;
using estore.Models;
using estore.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace estore.Controllers
{
    public class CartController : Controller
    {
        public readonly DataContex _context;
        public CartController(DataContex context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (!Functions.islogin())
                return RedirectToAction("Index", "Login");
            var userID = Functions._userid;
            var cart = _context.carts
                .Include(c => c.Products)
                .Include(c => c.ProductSize)
                .Where(c => c.UserId == userID)
                .ToList();
            return View(cart);
        }
        [HttpPost]
        public IActionResult AddToCart(Cart cart)
        {
            var product = _context.tblProducts.Find(cart.ProductId);
            cart.UserId = Functions._userid;
            cart.Price = product.Price;
            var item = _context.carts.FirstOrDefault(m => m.ProductId == cart.ProductId && m.SizeId == cart.SizeId && m.UserId == cart.UserId);
            if (item == null)
            {
                _context.carts.Add(cart);
            }
            else
            {
                item.Quantity += cart.Quantity;
                _context.carts.Update(item);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UpdateQuantity(int cartId, int quantity)
        {
            if (quantity < 0)
            {
                return RedirectToAction("Index");
            }
            var cartitem = _context.carts.FirstOrDefault(o => o.CartId == cartId && o.UserId == Functions._userid);
            if (cartitem != null)
            {
                cartitem.Quantity = quantity;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
  
        [HttpPost]
        public IActionResult DeleteCart(int cartId)
        {
            var item = _context.carts.FirstOrDefault(c => c.CartId == cartId);
            if (item != null)
            {
                _context.carts.Remove(item);
                _context.SaveChanges();
            }

            return RedirectToAction("Index"); //hoặc "ViewCart" tùy theo tên action hiển thị giỏ hàng 
        }
    }
}
