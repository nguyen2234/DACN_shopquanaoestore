using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estore.Models
{
    [Table("tblProducts")]
    public class TblProducts
    {
        [Key]
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Contents { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public int CategoriId { get; set; }
        //public int Stock { get; set; }
        public string? Images { get; set; }
        //[NotMapped]
        //public IFormFile? ImageFile { get; set; }
        public string? Link { get; set; }
        public bool? IsActive { get; set; }
       // danh sách kích thước
       public List<TblProductSize>? ProductSizes { get; set; }
    }
}
