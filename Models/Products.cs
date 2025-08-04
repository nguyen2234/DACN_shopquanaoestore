using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estore.Models
{
    [Table("tblProducts")]
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? Contents { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]

        public int CategoriId { get; set; }

        public Categori Categori { get; set; } = null!;
        public string? Images { get; set; }
        public string? Link { get; set; }
        public bool? IsActive { get; set; }
        public ICollection<ProductDetails> ProductDetails { get; set; } = new List<ProductDetails>();
      

    }
}
