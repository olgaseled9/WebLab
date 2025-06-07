namespace WebLab.Models;

public class ProductListModel<T>
{
    public IEnumerable<Product> Products { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
}
