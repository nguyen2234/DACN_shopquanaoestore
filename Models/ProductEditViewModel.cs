using System.ComponentModel.DataAnnotations;

public class ProductEditViewModel
{
    public int ProductId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
    public string Name { get; set; } = "";

    public string? Title { get; set; }
    public string? Contents { get; set; }
    public int Price { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn danh mục")]
    [Range(1, int.MaxValue, ErrorMessage = "Danh mục không hợp lệ")]
    public int CategoriId { get; set; }

    public bool? IsActive { get; set; }

    public string? Images { get; set; }
}
