
using System.ComponentModel.DataAnnotations.Schema;
using InventoryTracker.Enums;
using static InventoryTracker.Enums.TransactionTypes;

namespace RoboostAssessment.DTO.TransactionDTOs
{
    public class GetTTansactionToReportsDTO
    {
        public DateTime CreatedDate { get; set; }
        //public bool IsArchived { get; set; }
        public int Quantity { get; set; }
        public TransactionsTypeEnum TransactionsType { get; set; }

        public string UserName { get; set; }
        public string WarehouseName { get; set; }



        public int WarehouseId { get; set; }


        public string ProductName { get; set; }
        public int ProductID { get; set; }
    }
}
