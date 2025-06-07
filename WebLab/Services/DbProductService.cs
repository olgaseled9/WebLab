using WebLab.Models;
using Microsoft.EntityFrameworkCore;
using WebLab.Data;
using WebLab.Entities;

namespace WebLab.Services;

public class DbProductService
{
    private readonly IConfiguration _config;
    private readonly ApplicationDbContext _context;

    public DbProductService(IConfiguration config, ApplicationDbContext context)
    {
        _config = config;
        _context = context;
    }

    public async Task<ProductListModel<Dish>> GetProductListAsync(string? category, int pageNo = 1)
    {
        // Валидация pageNo
        if (pageNo < 1) pageNo = 1;


        string? normalizedCategory = category?.ToUpper();

        IQueryable<Dish> query = _context.Dishes.Include(d => d.Category);

        if (!string.IsNullOrEmpty(normalizedCategory))
        {
            query = query.Where(d => d.Category.NormalizedName == normalizedCategory);
        }

        int itemsPerPage = Math.Max(_config.GetValue("ItemsPerPage", 3), 1);
        int totalItems = await query.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

        var items = await query
            .OrderBy(d => d.Id)
            .Skip((pageNo - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToListAsync();

        return new ProductListModel<Dish>
        {
            Products = items,
            CurrentPage = pageNo,
            TotalPages = totalPages
        };
    }
}