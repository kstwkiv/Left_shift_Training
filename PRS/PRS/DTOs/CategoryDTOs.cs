using System.ComponentModel.DataAnnotations;

namespace PRS.DTOs
{
    public class CategoryDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int? ParentCategoryId { get; set; }
        public string ParentCategoryName { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int ProductCount { get; set; }
        public List<CategoryBasicDto> SubCategories { get; set; } = new();
    }

    public class CategorySummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int ProductCount { get; set; }
    }

    public class CategoryTreeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public List<CategoryTreeDto> Children { get; set; } = new();
    }

    public class CreateCategoryDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Url]
        public string ImageUrl { get; set; } = string.Empty;

        public int? ParentCategoryId { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class UpdateCategoryDto
    {
        [StringLength(100, MinimumLength = 2)]
        public string? Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Url]
        public string? ImageUrl { get; set; }

        public int? ParentCategoryId { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
    }
}
