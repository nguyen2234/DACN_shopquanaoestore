using System.ComponentModel.DataAnnotations;

public class ProductIndexViewModel
{
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public int Price { get; set; }
    
    public string? Categori { get; set; }
    public bool? IsActive { get; set; }

    public int TotalQuantity { get; set; }
    public string? Images { get; set; }
}
