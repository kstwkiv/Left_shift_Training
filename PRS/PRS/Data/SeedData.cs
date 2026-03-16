
using PRS.Data;
using PRS.Entities;

namespace PRS.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(AppDbContext context)
        {
            if (context.Categories.Any()) return;   // Already seeded

            // ── Categories ────────────────────────────────────────────────────
            var electronics = new Category { Name = "Electronics", Description = "Electronic devices and accessories", DisplayOrder = 1, IsActive = true, CreatedAt = DateTime.UtcNow };
            var clothing = new Category { Name = "Clothing", Description = "Fashion and apparel", DisplayOrder = 2, IsActive = true, CreatedAt = DateTime.UtcNow };
            var books = new Category { Name = "Books", Description = "Books, e-books, and audiobooks", DisplayOrder = 3, IsActive = true, CreatedAt = DateTime.UtcNow };

            context.Categories.AddRange(electronics, clothing, books);
            await context.SaveChangesAsync();

            // Sub-categories
            var smartphones = new Category { Name = "Smartphones", ParentCategoryId = electronics.Id, DisplayOrder = 1, IsActive = true, CreatedAt = DateTime.UtcNow };
            var laptops = new Category { Name = "Laptops", ParentCategoryId = electronics.Id, DisplayOrder = 2, IsActive = true, CreatedAt = DateTime.UtcNow };
            var menClothing = new Category { Name = "Men's Clothing", ParentCategoryId = clothing.Id, DisplayOrder = 1, IsActive = true, CreatedAt = DateTime.UtcNow };
            var techBooks = new Category { Name = "Tech Books", ParentCategoryId = books.Id, DisplayOrder = 1, IsActive = true, CreatedAt = DateTime.UtcNow };

            context.Categories.AddRange(smartphones, laptops, menClothing, techBooks);
            await context.SaveChangesAsync();

            // ── Suppliers ─────────────────────────────────────────────────────
            var techSupplier = new Supplier { Name = "TechCorp Supplies", ContactEmail = "orders@techcorp.com", ContactPhone = "+1-800-TECH", PaymentTerms = "Net 30", Rating = 4.8, IsActive = true };
            var fashionSupplier = new Supplier { Name = "Fashion Wholesale", ContactEmail = "orders@fashionwh.com", ContactPhone = "+1-800-FASH", PaymentTerms = "Net 15", Rating = 4.5, IsActive = true };

            context.Suppliers.AddRange(techSupplier, fashionSupplier);
            await context.SaveChangesAsync();

            // ── Products ──────────────────────────────────────────────────────
            var iphone = new Product
            {
                Name = "iPhone 15 Pro",
                Description = "Apple iPhone 15 Pro with titanium design, A17 Pro chip, and advanced camera system.",
                Price = 1199.00m,
                DiscountedPrice = 1099.00m,
                CategoryId = smartphones.Id,
                SupplierId = techSupplier.Id,
                SKU = "APPL-IP15P-256",
                ImageUrl = "https://example.com/iphone15pro.jpg",
                Tags = "[\"smartphone\",\"apple\",\"ios\",\"5g\"]",
                Specifications = "{\"Chip\":\"A17 Pro\",\"Storage\":\"256GB\",\"Display\":\"6.1-inch Super Retina XDR\",\"Camera\":\"48MP Main + 12MP Ultra Wide + 12MP Telephoto\"}",
                IsFeatured = true,
                IsActive = true,
                AverageRating = 4.8,
                ReviewCount = 124,
                SoldCount = 350,
                CreatedAt = DateTime.UtcNow.AddDays(-90)
            };

            var laptop = new Product
            {
                Name = "MacBook Pro 14\"",
                Description = "MacBook Pro 14-inch with M3 Pro chip, stunning Liquid Retina XDR display.",
                Price = 1999.00m,
                CategoryId = laptops.Id,
                SupplierId = techSupplier.Id,
                SKU = "APPL-MBP14-M3P",
                ImageUrl = "https://example.com/macbookpro14.jpg",
                Tags = "[\"laptop\",\"apple\",\"macos\",\"professional\"]",
                Specifications = "{\"Chip\":\"M3 Pro\",\"RAM\":\"18GB\",\"Storage\":\"512GB SSD\",\"Display\":\"14.2-inch Liquid Retina XDR\"}",
                IsFeatured = true,
                IsActive = true,
                AverageRating = 4.9,
                ReviewCount = 87,
                SoldCount = 210,
                CreatedAt = DateTime.UtcNow.AddDays(-60)
            };

            var tshirt = new Product
            {
                Name = "Premium Cotton T-Shirt",
                Description = "Soft, breathable 100% organic cotton t-shirt. Available in multiple colors.",
                Price = 29.99m,
                DiscountedPrice = 24.99m,
                CategoryId = menClothing.Id,
                SupplierId = fashionSupplier.Id,
                SKU = "FASH-TSH-ORG-M",
                ImageUrl = "https://example.com/tshirt.jpg",
                Tags = "[\"clothing\",\"organic\",\"cotton\",\"casual\"]",
                Specifications = "{\"Material\":\"100% Organic Cotton\",\"Fit\":\"Regular\",\"Care\":\"Machine Wash Cold\"}",
                IsFeatured = false,
                IsActive = true,
                AverageRating = 4.3,
                ReviewCount = 45,
                SoldCount = 980,
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            };

            context.Products.AddRange(iphone, laptop, tshirt);
            await context.SaveChangesAsync();

            // ── Inventories ───────────────────────────────────────────────────
            context.Inventories.AddRange(
                new Inventory { ProductId = iphone.Id, Quantity = 85, ReservedQuantity = 5, ReorderPoint = 20, WarehouseLocation = "A1-S3", LastRestockedAt = DateTime.UtcNow.AddDays(-10) },
                new Inventory { ProductId = laptop.Id, Quantity = 42, ReservedQuantity = 2, ReorderPoint = 10, WarehouseLocation = "A2-S1", LastRestockedAt = DateTime.UtcNow.AddDays(-5) },
                new Inventory { ProductId = tshirt.Id, Quantity = 320, ReservedQuantity = 15, ReorderPoint = 50, WarehouseLocation = "B3-S5", LastRestockedAt = DateTime.UtcNow.AddDays(-3) }
            );
            await context.SaveChangesAsync();

            // ── Product Attributes ────────────────────────────────────────────
            context.ProductAttributes.AddRange(
                new ProductAttribute { ProductId = iphone.Id, Key = "Color", Value = "Natural Titanium" },
                new ProductAttribute { ProductId = iphone.Id, Key = "Connectivity", Value = "5G, Wi-Fi 6E, Bluetooth 5.3" },
                new ProductAttribute { ProductId = laptop.Id, Key = "Color", Value = "Space Black" },
                new ProductAttribute { ProductId = laptop.Id, Key = "Battery", Value = "Up to 18 hours" }
            );

            // ── Product Reviews ───────────────────────────────────────────────
            context.ProductReviews.AddRange(
                new ProductReview { ProductId = iphone.Id, ReviewerName = "Alice Johnson", Rating = 5, Title = "Best phone ever!", Comment = "Incredible camera and performance. Titanium build feels premium.", IsApproved = true, CreatedAt = DateTime.UtcNow.AddDays(-20) },
                new ProductReview { ProductId = iphone.Id, ReviewerName = "Bob Williams", Rating = 4, Title = "Great but pricey", Comment = "Excellent device but the price is steep. Still worth it.", IsApproved = true, CreatedAt = DateTime.UtcNow.AddDays(-15) },
                new ProductReview { ProductId = laptop.Id, ReviewerName = "Carol Martinez", Rating = 5, Title = "Work powerhouse", Comment = "M3 Pro handles everything I throw at it without breaking a sweat.", IsApproved = true, CreatedAt = DateTime.UtcNow.AddDays(-10) },
                new ProductReview { ProductId = tshirt.Id, ReviewerName = "David Lee", Rating = 4, Title = "Very comfortable", Comment = "Great quality organic cotton. Fit is true to size.", IsApproved = true, CreatedAt = DateTime.UtcNow.AddDays(-7) }
            );

            await context.SaveChangesAsync();
        }
    }
}
