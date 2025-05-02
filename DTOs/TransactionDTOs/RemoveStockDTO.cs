using System.ComponentModel.DataAnnotations;
using static InventoryTracker.Enums.TransactionTypes;

namespace RoboostAssessment.DTO.TransactionDTOs
{
    public class RemoveStockDTO
    {
        public string UserName { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Quanitnty must be more than 0")]

        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public TransactionsTypeEnum TransactionsType { get; set; }

        public int WarehouseId { get; set; }
    }
}
