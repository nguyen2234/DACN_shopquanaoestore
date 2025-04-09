using estore.Models;
using Microsoft.AspNetCore.Mvc;

namespace estore.Components
{
    [ViewComponent(Name = "Slide")]
    public class SlideViewComponents : ViewComponent
    {
        private readonly DataContex _context;
        public SlideViewComponents(DataContex context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listofSlide = (from p in _context.slides
                               where (p.IsActive == true)
                               select p).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", listofSlide));
        }
    }
}
