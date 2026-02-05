namespace InventoryManagement.Repository.Services;

public class ProductRepository : IProductRepository
{
    private readonly InventoryDBContext _context;
    public ProductRepository(InventoryDBContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Product>> SearchAsync(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return await _context.Products.ToListAsync();
        }

        return await _context.Products.Where(product => (!string.IsNullOrEmpty(product.Name) && product.Name.Contains(query)) || (!string.IsNullOrEmpty(product.Description) && product.Description.Contains(query))).ToListAsync();
    }

    public async Task<decimal?> GetTotalInventoryValueAsync()
    {
        return await _context.Products.SumAsync(p => p.Price);
    }
}
