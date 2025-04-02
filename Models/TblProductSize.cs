using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estore.Models
{
    [Table("tblProductSize")]
    public class TblProductSize
    {

        public int Id { get; set; }
        public string? Name { get; set; }


    }
}
