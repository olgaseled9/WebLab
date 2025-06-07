using WebLab.Entities;

namespace WebLab.Models;

public class ProductListModel<T>

{
    public IEnumerable<T> Products { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}