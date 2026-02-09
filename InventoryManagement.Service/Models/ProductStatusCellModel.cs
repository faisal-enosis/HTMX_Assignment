namespace InventoryManagement.Service.Models;

public class ProductStatusCellModel
{
    public List<string> StatusCellIds { get; set; } = [];
    public ProductStatus Status { get; set; }
}
