using Microsoft.EntityFrameworkCore;
using PRS.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PRS.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // SKU must be unique across all products
            mb.Entity<Product>().HasIndex(p => p.SKU).IsUnique();

            // Product → Category (many-to-one, restrict delete)
            mb.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Product → Supplier (many-to-one, optional)
            mb.Entity<Product>()
                .HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // Product → Inventory (one-to-one)
            mb.Entity<Product>()
                .HasOne(p => p.Inventory)
                .WithOne(i => i.Product)
                .HasForeignKey<Inventory>(i => i.ProductId);

            // Category self-referencing hierarchy
            mb.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // Order → OrderItems (one-to-many)
            mb.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // InventoryTransaction → Inventory
            mb.Entity<InventoryTransaction>()
                .HasOne(t => t.Inventory)
                .WithMany(i => i.Transactions)
                .HasForeignKey(t => t.InventoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Decimal precision for currency columns
            mb.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
            mb.Entity<Product>().Property(p => p.DiscountedPrice).HasPrecision(18, 2);
            mb.Entity<OrderItem>().Property(oi => oi.UnitPrice).HasPrecision(18, 2);
            mb.Entity<OrderItem>().Property(oi => oi.TotalPrice).HasPrecision(18, 2);
            mb.Entity<Order>().Property(o => o.SubTotal).HasPrecision(18, 2);
            mb.Entity<Order>().Property(o => o.TaxAmount).HasPrecision(18, 2);
            mb.Entity<Order>().Property(o => o.ShippingAmount).HasPrecision(18, 2);
            mb.Entity<Order>().Property(o => o.TotalAmount).HasPrecision(18, 2);
        }
    }
}
