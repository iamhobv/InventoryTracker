using InventoryTracker.Enums;
using static InventoryTracker.Enums.TransactionTypes;

namespace InventoryTracker.DTOs.ReportDTOs
{
    public class AddTransactionHistoryDTO
    {
        public string UserName { get; set; }
        public int? CategoryId { get; set; }
        public int? ProductId { get; set; }
        public DateTime? StratingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public TransactionsTypeEnum? TransactionsType { get; set; }
        public bool? IsArchive { get; set; }
    }
}
