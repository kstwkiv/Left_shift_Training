using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRS.Services.Interfaces;
using PRS.Data;
using PRS.DTOs;
using PRS.Entities;
using PRS.Helpers.QueryObjects;
using PRS.Helpers.Extensions;  

namespace PRS.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ProductQuery _productQuery;
        private readonly ILogger<ProductService> _logger;

        public ProductService(
            AppDbContext context,
            IMapper mapper,
            ILogger<ProductService> logger)
        {
            _context = context;
            _mapper = mapper;
            _productQuery = new ProductQuery();
            _logger = logger;
        }

        // ── GET BY ID ─────────────────────────────────────────────────────────

        public async Task<ProductDetailDto> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Include(p => p.Inventory)
                .Include(p => p.Attributes)
                .Include(p => p.Reviews)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new KeyNotFoundException($"Product with ID {id} was not found.");

            return product.ToDetailDto();
        }

        // ── GET ALL (filtered, sorted, paginated) ─────────────────────────────

        public async Task<PagedResultDto<ProductSummaryDto>> GetAllProductsAsync(
            ProductFilterDto filter, int page = 1, int size = 10)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Inventory)
                .AsNoTracking();

            query = _productQuery.ApplyFilter(query, filter);

            // Count BEFORE pagination
            var total = await query.CountAsync();

            query = _productQuery.ApplySorting(query, filter?.SortBy, filter?.SortDescending ?? false);

            var products = await _productQuery
                .ApplyPagination(query, page, size)
                .ToListAsync();

            return new PagedResultDto<ProductSummaryDto>
            {
                Items = products.Select(p => p.ToSummaryDto()).ToList(),
                TotalCount = total,
                PageNumber = page,
                PageSize = size,
                TotalPages = (int)Math.Ceiling(total / (double)size)
            };
        }

        // ── CREATE ────────────────────────────────────────────────────────────

        public async Task<ProductDetailDto> CreateProductAsync(CreateProductDto dto)
        {
            // Business rule: SKU must be unique
            if (!await IsSkuUniqueAsync(dto.Sku))
                throw new InvalidOperationException($"SKU '{dto.Sku}' already exists.");

            // Business rule: category must exist
            if (await _context.Categories.FindAsync(dto.CategoryId) == null)
                throw new KeyNotFoundException($"Category {dto.CategoryId} was not found.");

            // Validate supplier if provided
            if (dto.SupplierId.HasValue &&
                await _context.Suppliers.FindAsync(dto.SupplierId.Value) == null)
                throw new KeyNotFoundException($"Supplier {dto.SupplierId} was not found.");

            // Map DTO → Entity
            var product = _mapper.Map<Product>(dto);
            product.CreatedAt = DateTime.UtcNow;
            product.PublishedAt = DateTime.UtcNow;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Create associated inventory record
            _context.Inventories.Add(new Inventory
            {
                ProductId = product.Id,
                Quantity = 0,
                ReorderPoint = dto.MinimumStockQuantity,
                LastRestockedAt = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

            _logger.LogInformation("Product created: {Name} (SKU: {Sku})", product.Name, product.SKU);

            // Reload from DB to include navigation properties
            return await GetProductByIdAsync(product.Id);
        }

        // ── UPDATE ────────────────────────────────────────────────────────────

        public async Task<ProductDetailDto> UpdateProductAsync(int id, UpdateProductDto dto)
        {
            var product = await _context.Products.FindAsync(id)
                ?? throw new KeyNotFoundException($"Product with ID {id} was not found.");

            // Validate category if being changed
            if (dto.CategoryId.HasValue &&
                await _context.Categories.FindAsync(dto.CategoryId.Value) == null)
                throw new KeyNotFoundException($"Category {dto.CategoryId} was not found.");

            // AutoMapper skips null values via ForAllMembers condition in MappingProfile
            _mapper.Map(dto, product);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Product updated: ID {Id}", id);

            return await GetProductByIdAsync(id);
        }

        // ── DELETE (hard) ─────────────────────────────────────────────────────

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Product hard-deleted: ID {Id}", id);
            return true;
        }

        // ── SOFT DELETE ───────────────────────────────────────────────────────

        public async Task<bool> SoftDeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            product.IsActive = false;
            product.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Product soft-deleted: ID {Id}", id);
            return true;
        }

        // ── COLLECTIONS ───────────────────────────────────────────────────────

        public async Task<List<ProductSummaryDto>> GetFeaturedProductsAsync(int count = 10)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Inventory)
                .Where(p => p.IsFeatured && p.IsActive)
                .OrderByDescending(p => p.AverageRating)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();

            return products.Select(p => p.ToSummaryDto()).ToList();
        }

        public async Task<List<ProductSummaryDto>> GetBestSellingProductsAsync(int count = 10)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Inventory)
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.SoldCount)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();

            return products.Select(p => p.ToSummaryDto()).ToList();
        }

        public async Task<List<ProductSummaryDto>> GetNewArrivalsAsync(int days = 30, int count = 10)
        {
            var cutoff = DateTime.UtcNow.AddDays(-days);

            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Inventory)
                .Where(p => p.IsActive && p.CreatedAt >= cutoff)
                .OrderByDescending(p => p.CreatedAt)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();

            return products.Select(p => p.ToSummaryDto()).ToList();
        }

        public async Task<List<ProductSummaryDto>> GetRelatedProductsAsync(int productId, int count = 5)
        {
            var product = await _context.Products.FindAsync(productId)
                ?? throw new KeyNotFoundException($"Product with ID {productId} was not found.");

            var related = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Inventory)
                .Where(p => p.CategoryId == product.CategoryId
                         && p.Id != productId
                         && p.IsActive)
                .OrderByDescending(p => p.AverageRating)
                .Take(count)
                .AsNoTracking()
                .ToListAsync();

            return related.Select(p => p.ToSummaryDto()).ToList();
        }

        // ── INVENTORY ─────────────────────────────────────────────────────────

        public async Task<InventoryStatusDto> GetProductInventoryAsync(int productId)
        {
            var inventory = await _context.Inventories
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.ProductId == productId)
                ?? throw new KeyNotFoundException($"Inventory for product {productId} was not found.");

            return inventory.ToStatusDto();
        }

        public async Task<bool> UpdateInventoryAsync(int id, UpdateInventoryDto dto)
        {
            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(i => i.ProductId == id);

            if (inventory == null) return false;

            var previousQty = inventory.Quantity;
            inventory.Quantity = dto.Quantity;
            inventory.LastRestockedAt = DateTime.UtcNow;
            inventory.LastUpdatedAt = DateTime.UtcNow;

            // Record the transaction
            _context.InventoryTransactions.Add(new InventoryTransaction
            {
                InventoryId = inventory.Id,
                Type = dto.Quantity > previousQty
                              ? TransactionType.Purchase
                              : TransactionType.Adjustment,
                Quantity = Math.Abs(dto.Quantity - previousQty),
                Notes = dto.Notes,
                CreatedBy = "System",
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return true;
        }

        // ── VALIDATION HELPERS ────────────────────────────────────────────────

        public async Task<bool> IsSkuUniqueAsync(string sku, int? excludeId = null)
        {
            var query = _context.Products.Where(p => p.SKU == sku.ToUpperInvariant());
            if (excludeId.HasValue)
                query = query.Where(p => p.Id != excludeId.Value);

            return !await query.AnyAsync();
        }

        public async Task<bool> IsProductAvailableAsync(int productId, int quantity)
        {
            var inventory = await _context.Inventories
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.ProductId == productId);

            return inventory != null && inventory.AvailableQuantity >= quantity;
        }
    }
}
