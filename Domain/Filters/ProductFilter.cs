namespace GMS_Backend.Domain.Filters;

public class ProductFilter
{
    public string? Name { get; set; }

    public string? Brand { get; set; }

    public Guid? CategoryId { get; set; }

    public Guid? SubcategoryId { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public string? SortBy { get; set; }

    public string? CategoryName { get; set; }

    public string? SubcategoryName { get; set; }
}