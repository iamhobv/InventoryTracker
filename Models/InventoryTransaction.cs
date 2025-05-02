using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
using static InventoryTracker.Enums.TransactionTypes;

namespace InventoryTracker.Models
{
    public class InventoryTransaction : BaseModel
    {
        public DateTime CreatedDate { get; set; }
        public int Quantity { get; set; }
        public TransactionsTypeEnum TransactionsType { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }


        [ForeignKey("Warehouse")]
        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }


        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public Product? Product { get; set; }
    }
}
