using System.ComponentModel.DataAnnotations;

namespace InventoryTracker.DTOs.ProductWarehouseDTOs
{
    public class AddProductWarehouseDTO
    {
        public int WarehouseId { get; set; }


        public int ProductID { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Quanitnty must be more than 0")]
        public int ProductQuantity { get; set; }
    }
}
