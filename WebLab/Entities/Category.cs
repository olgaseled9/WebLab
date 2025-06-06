namespace WebLab.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string NormalizedName { get; set; } = null!;
    public List<Dish> Dishes { get; set; } = new();
}
