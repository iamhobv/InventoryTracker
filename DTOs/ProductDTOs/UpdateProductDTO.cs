
namespace RoboostAssessment.DTO.ProductDTOs
{
    public class UpdateProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int? LowStockThreshold { get; set; }

        public int? CategoryId { get; set; }

        //public int? Quantity { get; set; }
        //public int? InventoryId { get; set; }

    }
}
