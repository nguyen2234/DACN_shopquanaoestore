using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estore.Models
{
    [Table("tblOrder")]
    public class Oder
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Address { get; set; }
        public string? Quantity { get; set; }
        public int  TotalPrice { get; set; }
        public int PaymethodId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Status { get; set; }
    }
}
