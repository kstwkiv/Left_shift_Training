using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRS.Data;
using PRS.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
namespace PRS.Controllers
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context) => _context = context;

        // ── GET api/v1/reports/sales ──────────────────────────────────────────
        [HttpGet("sales")]
        [ProducesResponseType(typeof(SalesReportDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<SalesReportDto>> GetSalesReport(
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? to = null)
        {
            var start = from ?? DateTime.UtcNow.AddMonths(-1);
            var end = to ?? DateTime.UtcNow;

            var orders = await _context.Orders
                .Include(o => o.Items)
                .Where(o => o.CreatedAt >= start && o.CreatedAt <= end)
                .AsNoTracking()
                .ToListAsync();

            var topProducts = orders
                .SelectMany(o => o.Items)
                .GroupBy(i => new { i.ProductId, i.ProductName })
                .Select(g => new ProductSalesDto
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.ProductName,
                    UnitsSold = g.Sum(i => i.Quantity),
                    Revenue = g.Sum(i => i.TotalPrice)
                })
                .OrderByDescending(p => p.Revenue)
                .Take(10)
                .ToList();

            var dailySales = orders
                .GroupBy(o => o.CreatedAt.Date)
                .Select(g => new DailySalesDto
                {
                    Date = g.Key,
                    Orders = g.Count(),
                    Revenue = g.Sum(o => o.TotalAmount)
                })
                .OrderBy(d => d.Date)
                .ToList();

            return Ok(new SalesReportDto
            {
                FromDate = start,
                ToDate = end,
                TotalOrders = orders.Count,
                TotalRevenue = orders.Sum(o => o.TotalAmount),
                AverageOrderValue = orders.Count > 0
                    ? orders.Average(o => o.TotalAmount) : 0,
                TotalItemsSold = orders.SelectMany(o => o.Items).Sum(i => i.Quantity),
                TopProducts = topProducts,
                DailySales = dailySales
            });
        }

        // ── GET api/v1/reports/inventory ──────────────────────────────────────
        [HttpGet("inventory")]
        [ProducesResponseType(typeof(InventoryReportDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<InventoryReportDto>> GetInventoryReport()
        {
            var inventories = await _context.Inventories
                .Include(i => i.Product)
                .AsNoTracking()
                .ToListAsync();

            var lowStock = inventories
                .Where(i => i.AvailableQuantity <= i.ReorderPoint && i.AvailableQuantity > 0)
                .Select(i => new LowStockItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name ?? string.Empty,
                    SKU = i.Product?.SKU ?? string.Empty,
                    AvailableQuantity = i.AvailableQuantity,
                    ReorderPoint = i.ReorderPoint
                })
                .ToList();

            return Ok(new InventoryReportDto
            {
                TotalProducts = inventories.Count,
                LowStockProducts = lowStock.Count,
                OutOfStockProducts = inventories.Count(i => i.AvailableQuantity == 0),
                TotalStockValue = inventories.Sum(i => i.Quantity),
                LowStockItems = lowStock
            });
        }

        // ── GET api/v1/reports/performance ────────────────────────────────────
        [HttpGet("performance")]
        [ProducesResponseType(typeof(PerformanceReportDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<PerformanceReportDto>> GetPerformanceReport()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Inventory)
                .AsNoTracking()
                .ToListAsync();

            var active = products.Where(p => p.IsActive).ToList();

            return Ok(new PerformanceReportDto
            {
                TotalProducts = products.Count,
                ActiveProducts = active.Count,
                AverageProductRating = active.Count > 0 ? active.Average(p => p.AverageRating) : 0,
                BestRatedProducts = active
                    .OrderByDescending(p => p.AverageRating)
                    .Take(5)
                    .Select(p => p.ToSummaryDto())
                    .ToList(),
                MostReviewedProducts = active
                    .OrderByDescending(p => p.ReviewCount)
                    .Take(5)
                    .Select(p => p.ToSummaryDto())
                    .ToList()
            });
        }
    }
