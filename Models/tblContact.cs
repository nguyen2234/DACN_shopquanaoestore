using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estore.Models
{
    [Table("tblContact")]
    public class TblContact
    {
        [Key]
        public int ContactID { get; set; }
        public string? FullName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Phone { get; set; }
        public string? InquiryMessage { get; set; }
    }
}

