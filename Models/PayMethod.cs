using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estore.Models
{
    [Table("tblPayMethod")]
    public class PayMethod
    {
        [Key]
        public int PaymethodId { get; set; }
        public string? Name { get; set; }
        public ICollection<Order> orders { get; set; }=new List<Order>();
    }
}
