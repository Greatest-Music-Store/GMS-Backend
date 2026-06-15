using Microsoft.EntityFrameworkCore;
using GMS_Backend.Domain.Models;
using GMS_Backend.Domain.Filters;

namespace GMS_Backend.Infrastructure.QueryExtensions;

public static class ProductFilterExtensions
{
    public static IQueryable<Product> Apply(this IQueryable<Product> query, ProductFilter filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(
                p => EF.Functions.ILike(
                    p.Name,
                    $"%{filter.Name}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.Brand))
        {
            query = query.Where(
                p => EF.Functions.ILike(
                    p.Brand,
                    $"%{filter.Brand}%"));
        }

        if (filter.MinPrice != null)
        {
            query = query.Where(
                p => p.Price >= filter.MinPrice);
        }

        if (filter.MaxPrice != null)
        {
            query = query.Where(
                p => p.Price <= filter.MaxPrice);
        }

        if (filter.CategoryId != null)
        {
            query = query.Where(
                p => p.CategoryId == filter.CategoryId);
        }

        if (filter.SubcategoryId != null)
        {
            query = query.Where(
                p => p.SubcategoryId == filter.SubcategoryId);
        }

        if (!string.IsNullOrWhiteSpace(filter.CategoryName))
        {
            query = query.Where(
                p => EF.Functions.ILike(
                    p.Category.Name,
                    $"%{filter.CategoryName}%"));
        }

        if (!string.IsNullOrWhiteSpace(filter.SubcategoryName))
        {
            query = query.Where(
                p => EF.Functions.ILike(
                    p.Subcategory.Name,
                    $"%{filter.SubcategoryName}%"));
        }

        query = filter.SortBy?.ToLower() switch
        {
            "name" => query.OrderBy(p => p.Name),
            "name_desc" => query.OrderByDescending(p => p.Name),

            "price" => query.OrderBy(p => p.Price),
            "price_desc" => query.OrderByDescending(p => p.Price),

            "brand" => query.OrderBy(p => p.Brand),
            "brand_desc" => query.OrderByDescending(p => p.Brand),

            _ => query
        };

        return query;
    }
}