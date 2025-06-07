using Microsoft.AspNetCore.Mvc;
using WebLab.Services;

namespace WebLab.Controllers;

public class ProductController : Controller
{
    private readonly MemoryProductService _productService;

    public ProductController(MemoryProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index(int pageNo = 1)
    {
        var model = await _productService.GetProductListAsync(pageNo);
        return View(model);
    }
}
