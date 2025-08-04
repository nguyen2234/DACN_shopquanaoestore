using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estore.Models
{
    [Table("tblCart")]
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Products Products { get; set; } = null!;

        public int Quantity { get; set; }
        public int Price { get; set; }
        public int SizeId { get; set; }
        [ForeignKey("SizeId")]
        public ProductSize ProductSize { get; set; } = null!;
    }
}
