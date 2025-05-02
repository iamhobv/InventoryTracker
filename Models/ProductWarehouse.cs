using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryTracker.Models
{
    public class ProductWarehouse : BaseModel
    {
        [ForeignKey("Warehouse")]
        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }


        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public Product? Product { get; set; }


        public int ProductQuantity { get; set; }
    }
}
