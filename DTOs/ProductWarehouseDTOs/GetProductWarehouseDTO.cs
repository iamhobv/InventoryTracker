using InventoryTracker.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryTracker.DTOs.ProductWarehouseDTOs
{
    public class GetProductWarehouseDTO
    {
        public int WarehouseId { get; set; }
        public string WarehouseLocation { get; set; }
        public string WarehouseName { get; set; }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }


        public int ProductQuantity { get; set; }
    }
 
}
