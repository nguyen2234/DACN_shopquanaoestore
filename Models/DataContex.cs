using Microsoft.EntityFrameworkCore;
using estore.Areas.Admin.Models;
namespace estore.Models
{
    public class DataContex :DbContext
    {
        public DataContex(DbContextOptions<DataContex> options) : base(options)
        {}
        public DbSet<TblMenu> tblMenus{ get; set; }
        public DbSet<tblAdminMenu> tblAdminMenu{ get; set; }
        public DbSet<TblProducts> tblProducts{ get; set; }
        public DbSet<TblProductDetails> TblProductDetails{ get; set; }
        public DbSet<TblProductSize> tblProductSizes{ get; set; } 
        public DbSet<AdminUser> AdminUsers{ get; set; }
    }
}