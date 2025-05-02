namespace RoboostAssessment.DTO.ProductDTOs
{
    public class GetProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int LowStockThreshold { get; set; }

        //public string CategoryName { get; set; }
        public int CategoryId { get; set; }

        //public string WarehouseName { get; set; }
        //public string WarehouseLocation { get; set; }
    }
}
