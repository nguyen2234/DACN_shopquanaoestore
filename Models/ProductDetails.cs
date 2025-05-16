using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estore.Models
{
    [Table("tblProductDetails")]
    public class ProductDetails
    {
        [Key]
        public int ProductDetailId { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Products Products { get; set; } = null!;

        public int SizeId { get; set; }
        [ForeignKey("SizeId")]
        public ProductSize ProductSize { get; set; } = null!;

        public string? Material { get; set; }
        public int Quantity { get; set; }

    }
}
