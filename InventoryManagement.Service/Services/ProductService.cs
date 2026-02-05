namespace InventoryManagement.Service.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IndexViewModel> GetIndexPageDataAsync()
    {
        var products = await _productRepository.GetAllAsync();
        var productVmList = products.Select(product => new ProductViewModel { Id = product.Id, Name = product.Name, Description = product.Description, Price = product.Price }).ToList();
        return new IndexViewModel { Products = productVmList, TotalPrice = productVmList.Sum(product => product.Price) };
    }

    public async Task<ProductViewModel?> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product == null ? null : new ProductViewModel { Id = product.Id, Name = product.Name, Description = product.Description, Price = product.Price };
    }

    public async Task<int> CreateProductAsync(ProductViewModel productVM)
    {
        var product = new Product { Name = productVM.Name, Description = productVM.Description, Price = productVM.Price };
        await _productRepository.AddAsync(product);
        return product.Id;
    }

    public async Task UpdateProductAsync(ProductViewModel productVM)
    {
        await _productRepository.UpdateAsync(new Product { Id = productVM.Id!.Value, Name = productVM.Name, Description = productVM.Description, Price = productVM.Price });
    }

    public async Task DeleteProductAsync(int id) 
    {
        await _productRepository.DeleteAsync(id);
    } 

    public async Task<List<ProductViewModel>> GetSearchDataAsync(string? query)
    {
        var products = await _productRepository.SearchAsync(query ?? string.Empty);
        var productVmList = products.Select(product => new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price
        }).ToList();

        return productVmList;
    }

    public async Task<decimal?> GetTotalInventoryValueAsync()
    {
        return await _productRepository.GetTotalInventoryValueAsync();
    }
}
