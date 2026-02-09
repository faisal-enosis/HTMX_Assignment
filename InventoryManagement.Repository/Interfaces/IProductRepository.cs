namespace InventoryManagement.Repository.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
    Task<List<Product>> SearchAsync(string query);
    Task<decimal?> GetTotalInventoryValueAsync();
    Task BulkUpdateProductStatusAsync(List<int> productIds, ProductStatus status);
}
