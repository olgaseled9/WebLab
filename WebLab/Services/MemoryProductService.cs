using WebLab.Entities;
using WebLab.Models;
using Microsoft.Extensions.Configuration;


namespace WebLab.Services;

using Microsoft.Extensions.Configuration;
using WebLab.Models;

public class MemoryProductService
{
    private readonly IConfiguration _config;
    private readonly List<Dish> _products;

    public MemoryProductService(IConfiguration config)
    {
        _config = config;
        _products = LoadProducts(); // Или загрузи откуда-то
    }

    public async Task<ProductListModel<Dish>> GetProductListAsync(int pageNo)
    {
        int itemsPerPage = _config.GetValue<int>("ItemsPerPage");

        int totalPages = (int)Math.Ceiling((double)_products.Count / itemsPerPage);
        var items = _products
            .Skip((pageNo - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToList();

        return new ProductListModel<Dish>
        {
            Items = items,
            CurrentPage = pageNo,
            TotalPages = totalPages
        };
    }
}
