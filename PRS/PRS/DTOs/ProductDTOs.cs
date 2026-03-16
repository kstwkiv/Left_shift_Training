using System.ComponentModel.DataAnnotations;
namespace PRS.DTOs
{
    public class ProductDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }

        // Computed — never stored in DB
        public decimal Savings => Price - (DiscountedPrice ?? Price);
        public int DiscountPercentage => DiscountedPrice.HasValue
            ? (int)((Price - DiscountedPrice.Value) / Price * 100) : 0;

        public string SKU { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }

        public CategoryBasicDto Category { get; set; } = new();
        public SupplierBasicDto? Supplier { get; set; }
        public InventoryStatusDto? Inventory { get; set; }

        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }

        public List<string> Tags { get; set; } = new();
        public Dictionary<string, string> Specifications { get; set; } = new();
        public List<ProductAttributeDto> Attributes { get; set; } = new();
        public List<ProductReviewDto> RecentReviews { get; set; } = new();

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>Lightweight summary — used in list/search responses (~70% smaller than detail).</summary>
    public class ProductSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }
        public bool IsInStock { get; set; }
    }

    // ─────────────────────────────────────────────────────────────────────────
    // REQUEST DTOs
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>POST body — all required fields with validation rules.</summary>
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be 3–100 characters")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Range(0, double.MaxValue)]
        [CompareWith("Price", IsLessThan = true,
            ErrorMessage = "Discounted price must be less than regular price")]
        public decimal? DiscountedPrice { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public int? SupplierId { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z0-9\-]+$",
            ErrorMessage = "SKU must be uppercase letters, numbers, and hyphens only")]
        [StringLength(50)]
        public string Sku { get; set; } = string.Empty;

        [Url(ErrorMessage = "Invalid image URL format")]
        public string ImageUrl { get; set; } = string.Empty;

        public bool IsFeatured { get; set; }

        [Range(0, int.MaxValue)]
        public int MinimumStockQuantity { get; set; } = 5;

        public List<string> Tags { get; set; } = new();
        public Dictionary<string, string> Specifications { get; set; } = new();
    }

    /// <summary>PUT body — all fields nullable to support partial updates.</summary>
    public class UpdateProductDto
    {
        [StringLength(100, MinimumLength = 3)]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal? Price { get; set; }

        public decimal? DiscountedPrice { get; set; }

        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsFeatured { get; set; }

        [Url]
        public string? ImageUrl { get; set; }

        public List<string>? Tags { get; set; }
        public Dictionary<string, string>? Specifications { get; set; }
    }

    // ─────────────────────────────────────────────────────────────────────────
    // FILTER / PAGINATION DTOs
    // ─────────────────────────────────────────────────────────────────────────

    /// <summary>Query-string parameters for GET /products.</summary>
    public class ProductFilterDto
    {
        public string? SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public List<int>? CategoryIds { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? IsOnSale { get; set; }
        public bool? InStock { get; set; }
        public bool? IsFeatured { get; set; }
        public int? MinRating { get; set; }
        public List<string>? Tags { get; set; }

        /// <summary>Price | Name | Rating | Newest | Popular</summary>
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; } = false;
    }

    /// <summary>Generic pagination envelope used by all list endpoints.</summary>
    public class PagedResultDto<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;
    }

    // ─────────────────────────────────────────────────────────────────────────
    // NESTED / SUPPORTING DTOs
    // ─────────────────────────────────────────────────────────────────────────

    public class CategoryBasicDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class SupplierBasicDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
    }

    public class InventoryStatusDto
    {
        public int Quantity { get; set; }
        public int ReservedQuantity { get; set; }
        public int AvailableQuantity { get; set; }
        public int ReorderPoint { get; set; }
        public bool IsLowStock => AvailableQuantity <= ReorderPoint;
        public string WarehouseLocation { get; set; } = string.Empty;
        public DateTime LastRestockedAt { get; set; }
    }

    public class UpdateInventoryDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int Quantity { get; set; }

        public string Notes { get; set; } = string.Empty;
    }

    public class ProductAttributeDto
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public class ProductReviewDto
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
