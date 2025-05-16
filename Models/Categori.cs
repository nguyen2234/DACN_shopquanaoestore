using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estore.Models
{
    [Table("tblCategori")]
    public class Categori
    {
        [Key]
        public int CategoriId { get; set; }
        public string? Name { get; set; }   
        public bool? IsActive { get; set; }
        public ICollection<Products> Products { get; set; } = new List<Products>();
    }
}
