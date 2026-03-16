using PRS.DTOs;
using PRS.Entities;
using System.Text.Json;

namespace PRS.Helpers.Extensions
{
    public static class ProductExtensions
    {
        private static readonly JsonSerializerOptions _jsonOpts = new() { PropertyNameCaseInsensitive = true };

        public static ProductDetailDto ToDetailDto(this Product p) => new()
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            DiscountedPrice = p.DiscountedPrice,
            SKU = p.SKU,
            ImageUrl = p.ImageUrl,
            IsActive = p.IsActive,
            IsFeatured = p.IsFeatured,
            AverageRating = p.AverageRating,
            ReviewCount = p.ReviewCount,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt,
            Category = new CategoryBasicDto
            {
                Id = p.Category?.Id ?? 0,
                Name = p.Category?.Name ?? string.Empty
            },
            Supplier = p.Supplier == null ? null : new SupplierBasicDto
            {
                Id = p.Supplier.Id,
                Name = p.Supplier.Name,
                ContactEmail = p.Supplier.ContactEmail
            },
            Inventory = p.Inventory?.ToStatusDto(),
            Tags = TryDeserialize<List<string>>(p.Tags) ?? new(),
            Specifications = TryDeserialize<Dictionary<string, string>>(p.Specifications) ?? new(),
            Attributes = p.Attributes.Select(a => new ProductAttributeDto
            {
                Key = a.Key,
                Value = a.Value
            }).ToList(),
            RecentReviews = p.Reviews
                .Where(r => r.IsApproved)
                .OrderByDescending(r => r.CreatedAt)
                .Take(5)
                .Select(r => r.ToDto())
                .ToList()
        };

        public static ProductSummaryDto ToSummaryDto(this Product p) => new()
        {
            Id = p.Id,
            Name = p.Name,
            // Truncate description to 100 chars for list views
            Description = p.Description?.Length > 100
                              ? p.Description[..100] + "…"
                              : p.Description ?? string.Empty,
            Price = p.Price,
            DiscountedPrice = p.DiscountedPrice,
            ImageUrl = p.ImageUrl,
            CategoryName = p.Category?.Name ?? string.Empty,
            AverageRating = p.AverageRating,
            ReviewCount = p.ReviewCount,
            IsInStock = (p.Inventory?.AvailableQuantity ?? 0) > 0
        };

        public static InventoryStatusDto ToStatusDto(this Inventory inv) => new()
        {
            Quantity = inv.Quantity,
            ReservedQuantity = inv.ReservedQuantity,
            AvailableQuantity = inv.AvailableQuantity,
            ReorderPoint = inv.ReorderPoint,
            WarehouseLocation = inv.WarehouseLocation,
            LastRestockedAt = inv.LastRestockedAt
        };

        public static ProductReviewDto ToDto(this ProductReview r) => new()
        {
            Id = r.Id,
            ReviewerName = r.ReviewerName,
            Rating = r.Rating,
            Title = r.Title,
            Comment = r.Comment,
            CreatedAt = r.CreatedAt
        };

        private static T? TryDeserialize<T>(string? json)
        {
            if (string.IsNullOrWhiteSpace(json)) return default;
            try { return JsonSerializer.Deserialize<T>(json, _jsonOpts); }
            catch { return default; }
        }
    }
}
