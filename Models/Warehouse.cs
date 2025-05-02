namespace InventoryTracker.Models
{
    public class Warehouse : BaseModel
    {
        public string Name { get; set; }
        public string Location { get; set; }


        public List<InventoryTransaction>? InventoryTransactions { get; set; }
        public List<ArchivedInventoryTransaction>? ArchivedInventoryTransaction { get; set; }
        public List<ProductWarehouse>? ProductWarehouse { get; set; }

    }
}
