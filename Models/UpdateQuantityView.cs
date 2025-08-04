public class UpdateQuantityViewModel
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public List<QuantityItem> Items { get; set; } = new List<QuantityItem>();

    public class QuantityItem
    {
        public int SizeId { get; set; }
        public string? SizeName { get; set; }
        public int Quantity { get; set; }
    }
}
