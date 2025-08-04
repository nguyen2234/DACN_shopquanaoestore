using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estore.Models
{
    [Table("tblOrderDetail")]
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int ProductSizeId { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; } = null!;

        [ForeignKey("ProductId")]
        public virtual Products Product { get; set; } = null!;

        [ForeignKey("ProductSizeId")]
        public virtual ProductSize ProductSize { get; set; } = null!;
    }
}
