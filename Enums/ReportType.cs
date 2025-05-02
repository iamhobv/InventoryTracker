namespace InventoryTracker.Enums
{
    public class ReportType
    {
        [Flags]

        public enum ReportTypeEnum
        {
            LowStockReport = 1,
            TransactionHistoryReport = 2,
        }
    }
}
