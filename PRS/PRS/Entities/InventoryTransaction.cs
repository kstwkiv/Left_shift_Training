namespace PRS.Entities
{
    public enum TransactionType { Purchase, Sale, Adjustment, Return, Transfer }

    public class InventoryTransaction
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public TransactionType Type { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual Inventory? Inventory { get; set; }
    }
}
