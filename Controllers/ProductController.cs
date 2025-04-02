using estore.Models;
using Microsoft.AspNetCore.Mvc;

namespace estore.Controllers
{

    public class ProductController : Controller
    {
        private readonly DataContex _contex;
        public ProductController(DataContex contex)
        {
            _contex = contex;
        }
        public IActionResult Index()
        {
            var pd = _contex.tblProducts.OrderBy(m => m.IsActive).ToList();
            return View(pd);
        }
        [Route("Product/details/{id}")]
        public IActionResult Details(int id)
        {
            var product = _contex.tblProducts.FirstOrDefault(o => o.Id == id);
            if (product == null)
                return NotFound();
            var size = _contex.tblProductSizes.ToList();
            var detail = _contex.TblProductDetails.Where(p => p.Id == id).ToList();
            ViewBag.Size = size;
            ViewBag.Detail = detail;
            ViewBag.Product = product;
            return View(product);

        }
    }
}
