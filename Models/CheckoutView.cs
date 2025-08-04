using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace estore.Models
{
    public class CheckoutView
    {
        public int userid { get; set; }
        public List<Cart> CartItems { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public List<SelectListItem> PaymentMethods { get; set; } = new();
        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Phone { get; set; }

        [Required]
        public string? Address { get; set; }
        public string? Note { get; set; }
        [Required]
        public int PaymethodId { get; set; }

    }
}
