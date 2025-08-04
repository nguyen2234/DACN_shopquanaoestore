using estore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace estore.Components
{
    [ViewComponent(Name = "Recomment")]
    public class RecommentViewComponents : ViewComponent
    {
        private readonly DataContex _context;
        public RecommentViewComponents(DataContex context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var grouped = await _context.TblProductDetails
             .Where(p => p.Products.IsActive == true)
             .GroupBy(p => p.ProductId)
             .Select(g => new
             {
                 ProductId = g.Key,
                 TotalSold = g.Sum(x => x.Quantity)
             })
             .OrderByDescending(x => x.TotalSold)
             .Take(8)
             .ToListAsync();

            var productIds = grouped.Select(g => g.ProductId).ToList();
            var products = await _context.tblProducts
                .Where(p => productIds.Contains(p.ProductId))
                .ToListAsync();

            var bestSellers = products
                .OrderByDescending(p => grouped.First(g => g.ProductId == p.ProductId).TotalSold)
                .ToList();



            return View("Default", bestSellers);
        }
    }
}


