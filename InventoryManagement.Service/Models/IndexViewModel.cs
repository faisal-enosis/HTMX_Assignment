namespace InventoryManagement.Services.Models;

public class IndexViewModel
{
    public List<ProductViewModel> Products { get; set; } = [];
    public decimal? TotalPrice { get; set; }
}
