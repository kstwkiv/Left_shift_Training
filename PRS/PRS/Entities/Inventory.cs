namespace PRS.Entities
{
    public class Inventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int ReservedQuantity { get; set; }
        public int ReorderPoint { get; set; } = 5;
        public string WarehouseLocation { get; set; } = string.Empty;
        public DateTime LastRestockedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }

        public int AvailableQuantity => Quantity - ReservedQuantity;

        // Navigation properties
        public virtual Product? Product { get; set; }
        public virtual ICollection<InventoryTransaction> Transactions { get; set; } = new List<InventoryTransaction>();
    }
}
