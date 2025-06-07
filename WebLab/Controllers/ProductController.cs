using Microsoft.AspNetCore.Mvc;
using WebLab.Services;

namespace WebLab.Controllers;

public class ProductController : Controller
{
    private readonly DbProductService _productService;

    public ProductController(DbProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index(string? category, int pageNo = 1)
    {
        var model = await _productService.GetProductListAsync(category, pageNo);
        return View(model);
    }
}
