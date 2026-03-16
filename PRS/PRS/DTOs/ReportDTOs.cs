namespace PRS.DTOs
{
    public class InventoryDetailDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int ReservedQuantity { get; set; }
        public int AvailableQuantity { get; set; }
        public int ReorderPoint { get; set; }
        public bool IsLowStock { get; set; }
        public string WarehouseLocation { get; set; } = string.Empty;
        public DateTime LastRestockedAt { get; set; }
        public List<InventoryTransactionDto> RecentTransactions { get; set; } = new();
    }

    public class InventoryTransactionDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    // ─── Report DTOs ──────────────────────────────────────────────────────────

    public class SalesReportDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageOrderValue { get; set; }
        public int TotalItemsSold { get; set; }
        public List<ProductSalesDto> TopProducts { get; set; } = new();
        public List<DailySalesDto> DailySales { get; set; } = new();
    }

    public class ProductSalesDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int UnitsSold { get; set; }
        public decimal Revenue { get; set; }
    }

    public class DailySalesDto
    {
        public DateTime Date { get; set; }
        public int Orders { get; set; }
        public decimal Revenue { get; set; }
    }

    public class InventoryReportDto
    {
        public int TotalProducts { get; set; }
        public int LowStockProducts { get; set; }
        public int OutOfStockProducts { get; set; }
        public int TotalStockValue { get; set; }
        public List<LowStockItemDto> LowStockItems { get; set; } = new();
    }

    public class LowStockItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int AvailableQuantity { get; set; }
        public int ReorderPoint { get; set; }
    }

    public class PerformanceReportDto
    {
        public int TotalProducts { get; set; }
        public int ActiveProducts { get; set; }
        public double AverageProductRating { get; set; }
        public List<ProductSummaryDto> BestRatedProducts { get; set; } = new();
        public List<ProductSummaryDto> MostReviewedProducts { get; set; } = new();
    }
}
