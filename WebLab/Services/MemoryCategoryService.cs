using WebLab.Entities;
using WebLab.Models;

namespace WebLab.Services;

public class MemoryCategoryService : ICategoryService
{
    public Task<ResponseData<List<Category>>> GetCategoryListAsync()
    {
        var categories = new List<Category>
        {
            new Category { Id = 1, Name = "Стартеры", NormalizedName = "starters" },
            new Category { Id = 2, Name = "Салаты", NormalizedName = "salads" },
            new Category { Id = 3, Name = "Напитки", NormalizedName = "drinks" }
        };

        return Task.FromResult(new ResponseData<List<Category>> { Data = categories });
    }
}