using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRS.Data;
using PRS.DTOs;
using PRS.Entities;

namespace PRS.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context) => _context = context;

        // ── GET api/v1/categories ─────────────────────────────────────────────
        [HttpGet]
        [ProducesResponseType(typeof(List<CategorySummaryDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategorySummaryDto>>> GetAll()
        {
            var categories = await _context.Categories
                .Include(c => c.Products)
                .Where(c => c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                .AsNoTracking()
                .ToListAsync();

            return Ok(categories.Select(c => new CategorySummaryDto
            {
                Id = c.Id,
                Name = c.Name,
                ImageUrl = c.ImageUrl,
                ProductCount = c.Products.Count(p => p.IsActive)
            }));
        }

        // ── GET api/v1/categories/tree ────────────────────────────────────────
        [HttpGet("tree")]
        [ProducesResponseType(typeof(List<CategoryTreeDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CategoryTreeDto>>> GetTree()
        {
            var all = await _context.Categories
                .Where(c => c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                .AsNoTracking()
                .ToListAsync();

            // Build tree from flat list — only root-level nodes
            var roots = BuildTree(all, null);
            return Ok(roots);
        }

        // ── GET api/v1/categories/{id} ────────────────────────────────────────
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CategoryDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDetailDto>> Get(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .Include(c => c.SubCategories)
                .Include(c => c.ParentCategory)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return NotFound(new { message = $"Category {id} not found." });

            return Ok(new CategoryDetailDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ImageUrl = category.ImageUrl,
                ParentCategoryId = category.ParentCategoryId,
                ParentCategoryName = category.ParentCategory?.Name ?? string.Empty,
                DisplayOrder = category.DisplayOrder,
                IsActive = category.IsActive,
                ProductCount = category.Products.Count(p => p.IsActive),
                SubCategories = category.SubCategories
                    .Where(s => s.IsActive)
                    .Select(s => new CategoryBasicDto { Id = s.Id, Name = s.Name })
                    .ToList()
            });
        }

        // ── POST api/v1/categories ────────────────────────────────────────────
        [HttpPost]
        [ProducesResponseType(typeof(CategoryDetailDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (dto.ParentCategoryId.HasValue &&
                await _context.Categories.FindAsync(dto.ParentCategoryId.Value) == null)
                return BadRequest(new { message = $"Parent category {dto.ParentCategoryId} not found." });

            var category = new Category
            {
                Name = dto.Name,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                ParentCategoryId = dto.ParentCategoryId,
                DisplayOrder = dto.DisplayOrder,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = category.Id },
                new CategoryDetailDto { Id = category.Id, Name = category.Name });
        }

        // ── PUT api/v1/categories/{id} ────────────────────────────────────────
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(CategoryDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound(new { message = $"Category {id} not found." });

            if (dto.Name != null) category.Name = dto.Name;
            if (dto.Description != null) category.Description = dto.Description;
            if (dto.ImageUrl != null) category.ImageUrl = dto.ImageUrl;
            if (dto.DisplayOrder.HasValue) category.DisplayOrder = dto.DisplayOrder.Value;
            if (dto.IsActive.HasValue) category.IsActive = dto.IsActive.Value;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Category updated.", id });
        }

        // ── DELETE api/v1/categories/{id} ─────────────────────────────────────
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return NotFound(new { message = $"Category {id} not found." });

            if (category.Products.Any())
                return BadRequest(new { message = "Cannot delete a category that contains products." });

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ── HELPER: build recursive tree ──────────────────────────────────────
        private static List<CategoryTreeDto> BuildTree(List<Category> all, int? parentId)
            => all
                .Where(c => c.ParentCategoryId == parentId)
                .Select(c => new CategoryTreeDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImageUrl = c.ImageUrl,
                    DisplayOrder = c.DisplayOrder,
                    Children = BuildTree(all, c.Id)
                })
                .ToList();
    }
}
