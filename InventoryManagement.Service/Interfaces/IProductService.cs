namespace InventoryManagement.Service.Interfaces;

public interface IProductService
{
    Task<IndexViewModel> GetIndexPageDataAsync();
    Task<ProductViewModel?> GetProductByIdAsync(int id);
    Task<int> CreateProductAsync(ProductViewModel vm);
    Task UpdateProductAsync(ProductViewModel vm);
    Task DeleteProductAsync(int id);
    Task<List<ProductViewModel>> GetSearchDataAsync(string? query);
    Task<decimal?> GetTotalInventoryValueAsync();
}
