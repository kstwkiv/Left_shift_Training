using PRS.DTOs;
using PRS.Entities;

namespace PRS.Helpers.QueryObjects
{
    /// <summary>
    /// Encapsulates all filter / sort / pagination logic for Product queries.
    /// Works against IQueryable so it can be unit-tested with in-memory lists.
    /// </summary>
    public class ProductQuery
    {
        public IQueryable<Product> ApplyFilter(IQueryable<Product> query, ProductFilterDto? filter)
        {
            if (filter == null) return query;

            // Text search across name, description, and SKU
            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                var term = filter.SearchTerm.ToLower();
                query = query.Where(p =>
                    p.Name.ToLower().Contains(term) ||
                    p.Description.ToLower().Contains(term) ||
                    p.SKU.ToLower().Contains(term));
            }

            // Single category
            if (filter.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == filter.CategoryId.Value);

            // Multiple categories (OR)
            if (filter.CategoryIds?.Any() == true)
                query = query.Where(p => filter.CategoryIds.Contains(p.CategoryId));

            // Price range
            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);

            // On-sale products only
            if (filter.IsOnSale == true)
                query = query.Where(p => p.DiscountedPrice != null && p.DiscountedPrice < p.Price);

            // In-stock filter
            if (filter.InStock == true)
                query = query.Where(p => p.Inventory != null && p.Inventory.AvailableQuantity > 0);

            // Featured flag
            if (filter.IsFeatured.HasValue)
                query = query.Where(p => p.IsFeatured == filter.IsFeatured.Value);

            // Minimum rating
            if (filter.MinRating.HasValue)
                query = query.Where(p => p.AverageRating >= filter.MinRating.Value);

            // Only show active products by default
            query = query.Where(p => p.IsActive);

            return query;
        }

        public IQueryable<Product> ApplySorting(
            IQueryable<Product> query, string? sortBy, bool desc = false)
            => sortBy?.ToLower() switch
            {
                "price" => desc ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                "name" => desc ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                "rating" => query.OrderByDescending(p => p.AverageRating),
                "newest" => query.OrderByDescending(p => p.CreatedAt),
                "popular" => query.OrderByDescending(p => p.SoldCount),
                _ => query.OrderBy(p => p.Name)
            };

        public IQueryable<Product> ApplyPagination(IQueryable<Product> query, int page, int size)
        {
            page = Math.Max(1, page);
            size = Math.Clamp(size, 1, 100);
            return query.Skip((page - 1) * size).Take(size);
        }
    }
}
