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
        var productVmList = products.Select(product => new ProductViewModel { Id = product.Id, Name = product.Name, Description = product.Description, Price = product.Price, Status = product.Status }).ToList();
        return new IndexViewModel { Products = productVmList, TotalPrice = productVmList.Sum(product => product.Price) };
    }

    public async Task<ProductViewModel?> GetProductByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product == null ? null : new ProductViewModel { Id = product.Id, Name = product.Name, Description = product.Description, Price = product.Price, Status = product.Status };
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
            Price = product.Price,
            Status = product.Status
        }).ToList();

        return productVmList;
    }

    public async Task<decimal?> GetTotalInventoryValueAsync()
    {
        return await _productRepository.GetTotalInventoryValueAsync();
    }

    public SelectList GetProductStatusAsDropdown()
    {
        return new SelectList(Enum.GetValues(typeof(ProductStatus)).Cast<ProductStatus>().Select(status => new { Id = (int)status, Name = Helper.GetEnumDisplayName(status) }), "Id", "Name");
    }

    public async Task<ProductStatusCellModel> BulkUpdateProductStatusAsync(List<int> productIds, ProductStatus status)
    {
        await _productRepository.BulkUpdateProductStatusAsync(productIds, status);
        var statusCellModel = new ProductStatusCellModel
        {
            StatusCellIds = productIds.Select(id => $"{id}-status").ToList(),
            Status = status
        };

        return statusCellModel;
    }
}
