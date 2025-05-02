using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryTracker.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int LowStockThreshold { get; set; }


        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }







        public List<InventoryTransaction>? InventoryTransactions { get; set; }
        public List<ProductWarehouse>? ProductWarehouse { get; set; }
        public List<ArchivedInventoryTransaction>? ArchivedInventoryTransaction { get; set; }
        public List<Notification>? Notifications { get; set; }
    }
}
