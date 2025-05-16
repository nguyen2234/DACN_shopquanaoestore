using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estore.Models
{
    [Table("tblProductSize")]
    public class ProductSize
    {
        [Key]
        public int SizeId { get; set; }
        public string? Size { get; set; }
        public ICollection<ProductDetails> ProductDetails { get; set; } = new List<ProductDetails>();

    }
}
