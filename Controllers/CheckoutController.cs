using estore.Models;
using Microsoft.AspNetCore.Mvc;
using estore.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using estore.Areas.Admin.Models;
namespace estore.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly DataContex _contex;
        public CheckoutController(DataContex contex)
        {
            _contex = contex;
        }
        public IActionResult Index()
        {
            var UserId = Functions._userid;

            var cart = _contex.carts.Where(i => i.UserId == UserId).Include(c => c.Products).Include(c => c.ProductSize).ToList();
            decimal totalamout = cart.Sum(i => i.Price * i.Quantity);
            var paymethod = _contex.payMethods
              .Select(m => new SelectListItem
              {
                  Value = m.PaymethodId.ToString(),
                  Text = m.Name
              }).ToList();
            var item = new CheckoutView
            {
                userid = UserId,
                CartItems = cart,
                TotalAmount = totalamout,
                PaymentMethods  = paymethod,
          
            };
            return View(item);
            

        }
        [HttpPost]
        public IActionResult Checkout(CheckoutView checkout)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction("Index");
            }
            int userid = Functions._userid;
            var cartitem = _contex.carts.Where(c => c.UserId == userid).ToList();
            var total = cartitem.Sum(c => c.Price * c.Quantity);
            var order = new Order
            {
                userid = userid,
                Name = checkout.Name,
                Email = Functions._email,
                Phone = checkout.Phone,
                Address = checkout.Address,
                Note = checkout.Note,
                TotalPrice = (int)total,
                PaymethodId = checkout.PaymethodId,
                CreatedAt = DateTime.Now,
                Status = "Chờ xác nhận"
            };

            _contex.orders.Add(order);
            _contex.SaveChanges();

            foreach (var item in cartitem)
            {
                var detail = new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    ProductSizeId = item.SizeId,
                    Quantity = item.Quantity,
                    Price = item.Price
                };
                _contex.orderDetail.Add(detail);
            }

            _contex.carts.RemoveRange(cartitem);
            _contex.SaveChanges();
            TempData["Success"] = $"Thanh toán thanh công";
            return RedirectToAction("History");
        }
        public IActionResult History()
        {
            int userId = Functions._userid;

            var orders = _contex.orders
                .Where(o => o.userid == userId)
                .Include(c=>c.PayMethod)
                .OrderByDescending(o => o.CreatedAt)
                .ToList();

            return View(orders);
        }

    }
}
