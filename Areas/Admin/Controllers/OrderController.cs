using estore.Models;
using estore.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace estore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly DataContex _context;
        public OrderController(DataContex context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (!ModelState.IsValid)
            {
                return NotFound();  
            }
            var item = _context.orders.Include(c => c.OrderDetails).ThenInclude(i => i.Product)
                .Include(c => c.OrderDetails).ThenInclude(i => i.ProductSize)
                .Include(k=>k.PayMethod).OrderByDescending(m => m.CreatedAt).ToList();
            return View(item);
                
        }
        public IActionResult Information(int? id)
        {
            if (id==null) {
                return NotFound();
            }
            var info = _context.orders.Include(c => c.OrderDetails).ThenInclude(i => i.Product)
                .Include(c => c.OrderDetails).ThenInclude(i => i.ProductSize)
                .Include(k=>k.PayMethod).FirstOrDefault(e=>e.Id== id);
            return View(info);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var order = _context.orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            _context.orderDetail.RemoveRange(order.OrderDetails); 
            _context.orders.Remove(order);                         
            _context.SaveChanges();

            TempData["Success"] = "Đã xoá đơn hàng thành công.";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult UpdateStatus(int orderId, string status)
        {
            var order = _context.orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null) return NotFound();
            
            if (order.Status == "Đã xác nhận" && status == "Đã hủy")
            {
                foreach (var detail in order.OrderDetails)
                {
                    var stock = _context.TblProductDetails.FirstOrDefault(p =>
                        p.ProductId == detail.ProductId &&
                        p.SizeId == detail.ProductSizeId);

                    if (stock != null)
                    {
                        stock.Quantity += detail.Quantity; 
                    }
                }
            }
            if (status == "Đã xác nhận" && order.Status != "Đã xác nhận")
            {
                foreach (var detail in order.OrderDetails)
                {
                    var item = _context.TblProductDetails.FirstOrDefault(p =>
                        p.ProductId == detail.ProductId &&
                        p.SizeId == detail.ProductSizeId);

                    if (item != null && item.Quantity >= detail.Quantity)
                    {
                        item.Quantity -= detail.Quantity;
                    }
                    else
                    {
                        TempData["Error"] = "Không đủ hàng trong kho để xác nhận đơn.";
                        return RedirectToAction("Information", new { id = orderId });
                    }
                }
            }
            
            order.Status = status;
            _context.SaveChanges();

            TempData["Success"] = $"Đơn hàng đã chuyển sang trạng thái: {status}";
            return RedirectToAction("Information", new { id = orderId });
        }



    }
}
