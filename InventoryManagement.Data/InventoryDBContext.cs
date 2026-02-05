namespace InventoryManagement.Data;

public class InventoryDBContext: DbContext
{
    public InventoryDBContext(DbContextOptions<InventoryDBContext> options)
    : base(options) { }

    public DbSet<Product> Products { get; set; } 
}
