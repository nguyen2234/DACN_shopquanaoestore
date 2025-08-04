using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estore.Models
{
    [Table("tblOrder")]
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int userid { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public int TotalPrice { get; set; }
        public int PaymethodId { get; set; }
        [ForeignKey("PaymethodId")]
        public PayMethod PayMethod { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? Status { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public string? Note { get; set; }

    }
}
