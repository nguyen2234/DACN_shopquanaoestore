using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estore.Models
{
    [Table("tblProductDetails")]
    public class TblProductDetails
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("TblProducts")]
        public int ProductsId { get; set; }
        [ForeignKey("TblProductSize")]
        public int SizeId { get; set; }
        public string? Material {  get; set; }
        public int Quantity { get; set; }
    }
}
