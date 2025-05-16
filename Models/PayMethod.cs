using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estore.Models
{
    [Table("tblPayMethod")]
    public class PayMethod
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Id")]
        public PayMethod payMethod { get; set; } = null!;
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
