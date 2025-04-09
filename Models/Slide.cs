using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace estore.Models
{
    [Table("Slide")]
    public class Slide
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Contents { get; set; }
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public bool? IsActive { get; set; }
    }
}
