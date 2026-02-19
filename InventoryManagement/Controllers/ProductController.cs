namespace InventoryManagement.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService) 
    {
        _productService = productService;
    } 

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var indexVm = await _productService.GetIndexPageDataAsync();
        return View(indexVm);
    }

    [HttpGet]
    [RequireHtmx]
    public async Task<IActionResult> ReadOnly(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var productVm = await _productService.GetProductByIdAsync(id);
        return PartialView("_ReadOnlyProductResponse", productVm);
    }

    [HttpPost]
    [RequireHtmx]
    public async Task<IActionResult> Create(ProductViewModel productVm)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("_AddProductErrorResponse", productVm);
        }

        productVm.Id = await _productService.CreateProductAsync(productVm);
        return PartialView("_CreateResponse", productVm);
    }

    [HttpGet]
    [RequireHtmx]
    public async Task<IActionResult> Edit(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) 
        {
            return NotFound();
        }

        return PartialView("_EditProductRow", product);
    }

    [HttpPut]
    [RequireHtmx]
    public async Task<IActionResult> Edit(ProductViewModel productVm)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("_EditProductRow", productVm);
        }

        await _productService.UpdateProductAsync(productVm);
        return PartialView("_EditResponse", productVm);
    }

    [HttpDelete]
    [RequireHtmx]
    public async Task<IActionResult> Delete(int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        await _productService.DeleteProductAsync(id);
        return PartialView("_DeleteResponse", id);
    }

    [HttpGet]
    [RequireHtmx]
    public async Task<IActionResult> Search(string? query)
    {
        var productVMList = await _productService.GetSearchDataAsync(query);
        return PartialView("_ProductTable", productVMList);
    }

    [HttpPut]
    [RequireHtmx]
    public async Task<IActionResult> BulkUpdateProductStatus(List<int> selectedIds, ProductStatus productStatus)
    {
        var statusCellModel = await _productService.BulkUpdateProductStatusAsync(selectedIds, productStatus);
        return PartialView("_ProductStatusCellOob", statusCellModel);
    }
}