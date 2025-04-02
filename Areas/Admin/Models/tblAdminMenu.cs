using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estore.Areas.Admin.Models
{
    [Table("tblAdminMenu")]
<<<<<<< HEAD:Areas/Admin/Models/tblAdminMenu.cs
    public class tblAdminMenu
=======
    public class AdminMenu
>>>>>>> 8f89f633277344c688e9ace82aab077c686073b2:Areas/Admin/Models/AdminMenu.cs
    {
        [Key]
        public long AdminMenuID { get; set; }
        public string? ItemName { get; set; }
        public int ItemLevel {  get; set; }
        public int ParentLevel { get; set; }
        public int ItemOrder { get; set; }
        public bool? IsActive { get; set; }
        public string? ItemTarget { get; set; }
        public string? AreaName { get; set; }
        public string? ControllerName { get; set; }
        public string? ActionName { get; set; }
        public string? Icon {  get; set; }
        public string? IdName { get; set; }
    }
}
