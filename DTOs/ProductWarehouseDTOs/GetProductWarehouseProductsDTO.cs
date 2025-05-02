namespace InventoryTracker.DTOs.ProductWarehouseDTOs
{
    public class GetProductWarehouseProductsDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }


        public int ProductQuantity { get; set; }
        public string Description { get; set; }
        public int LowStockThreshold { get; set; }

        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
