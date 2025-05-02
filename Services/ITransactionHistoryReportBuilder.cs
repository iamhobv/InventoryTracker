using InventoryTracker.Enums;
using RoboostAssessment.DTO.TransactionDTOs;
using static InventoryTracker.Enums.TransactionTypes;

namespace InventoryTracker.Services
{
    public interface ITransactionHistoryReportBuilder
    {
        ITransactionHistoryReportBuilder SetProduct(int? productId);
        ITransactionHistoryReportBuilder SetDateRange(DateTime? startDate, DateTime? endDate);

        ITransactionHistoryReportBuilder SetCategory(int? categoryId);
        ITransactionHistoryReportBuilder SetTransactionsType(TransactionsTypeEnum? transactionsType);
        Task<IEnumerable<GetTTansactionToReportsDTO>> Build();
    }
}
