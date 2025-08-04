using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estore.Models
{
    [Table("tblContact")]
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string? Name { get; set; }
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập nội dung liên hệ")]

        public string? Message { get; set; }
        public DateTime SentAt { get; set; }
        public int userid { get; set; }
        public bool? IsActive { get; set; }
    }
}
