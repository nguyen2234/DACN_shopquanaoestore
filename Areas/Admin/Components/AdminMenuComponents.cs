using estore.Models;
using Microsoft.AspNetCore.Mvc;

namespace estore.Areas.Admin.Components
{
    [ViewComponent(Name ="AdminMenu")]
    public class AdminMenuComponents :ViewComponent
    {
        private readonly DataContex _context;
        public AdminMenuComponents(DataContex context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var mnList =(from m in _context.tblAdminMenu
                         where (m.IsActive ==true)
                         select m).ToList();
            return await Task.FromResult((IViewComponentResult)View("Default", mnList));
        }
    }
}
