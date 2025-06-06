using WebLab.Entities;
using WebLab.Models;

namespace WebLab.Services;

public interface ICategoryService
{
    Task<ResponseData<List<Category>>> GetCategoryListAsync();
}
