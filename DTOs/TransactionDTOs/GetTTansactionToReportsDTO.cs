
using System.ComponentModel.DataAnnotations.Schema;
using InventoryTracker.Enums;

namespace RoboostAssessment.DTO.TransactionDTOs
{
    public class GetTTansactionToReportsDTO
    {
        public DateTime CreatedDate { get; set; }
        //public bool IsArchived { get; set; }
        public int Quantity { get; set; }
        public TransactionTypes TransactionsType { get; set; }

        public string UserName { get; set; }
        public string InventoryName { get; set; }



        public int InventoryId { get; set; }


        public string ProductName { get; set; }
        public int ProductID { get; set; }
    }
}
