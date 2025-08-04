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
        public IActionResult AddToCart(Cart cart)
        {
            cart.UserId = Functions._userid;

            var product = _context.tblProducts.Find(cart.ProductId);
            cart.Price = product.Price;


            var productDetail = _context.TblProductDetails.FirstOrDefault(pd =>
                pd.ProductId == cart.ProductId && pd.SizeId == cart.SizeId);

            if (productDetail == null)
            {

                TempData["Error"] = "Size không hợp lệ.";
                return RedirectToAction("Index", new { id = cart.ProductId });
            }

            var item = _context.carts.FirstOrDefault(m =>
                m.ProductId == cart.ProductId &&
                m.SizeId == cart.SizeId &&
                m.UserId == cart.UserId);

            int currentQty = item?.Quantity ?? 0;
            int total = currentQty + cart.Quantity;

            if (total > productDetail.Quantity)
            {
                TempData["Error"] = "Bạn đã thêm vượt quá số sản phẩm hiện có trong kho. Vui lòng kiểm tra giỏ hàng và đặt lại.";
                return RedirectToAction("Details", "Product", new { id = cart.ProductId });
            }

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
            if (quantity < 1)
            {
                TempData["Error"] = "Số lượng phải lớn hơn 0.";

                return RedirectToAction("Index");
            }
            var cartitem = _context.carts
            .Include(c => c.Products)
            .ThenInclude(p => p.ProductDetails)
            .Include(c => c.ProductSize)
             .FirstOrDefault(c => c.CartId == cartId && c.UserId == Functions._userid);
            if (cartitem == null)
            {
                TempData["Error"] = "Không tìm thấy sản phẩm trong giỏ hàng.";
                return RedirectToAction("Index");
            }

            var checksize = cartitem.Products.ProductDetails
                .FirstOrDefault(p => p.SizeId == cartitem.SizeId)?.Quantity ?? 0;
            
            if (quantity > checksize)
            {
                TempData["Error"] = $"Chỉ còn {checksize} sản phẩm {cartitem.Products.Name} size {cartitem.ProductSize.Size}.";
                return RedirectToAction("Index");
            }

            cartitem.Quantity = quantity;
            _context.SaveChanges();

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

            return RedirectToAction("Index");

        }
    }
}
