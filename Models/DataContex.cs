using Microsoft.EntityFrameworkCore;
using estore.Areas.Admin.Models;
namespace estore.Models
{
    public class DataContex : DbContext
    {
        public DataContex(DbContextOptions<DataContex> options) : base(options)
        { }
        public DbSet<Menu> tblMenus { get; set; }
        public DbSet<tblAdminMenu> tblAdminMenu { get; set; }
        public DbSet<Products> tblProducts { get; set; }
        public DbSet<ProductDetails> TblProductDetails { get; set; }
        public DbSet<ProductSize> tblProductSizes { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Slide> slides { get; set; }
        public DbSet<Categori> categoris { get; set; }
        public DbSet<Cart> carts { get; set; }
       
    }
}