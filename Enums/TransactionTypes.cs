namespace InventoryTracker.Enums
{
    public class TransactionTypes
    {
        [Flags]
        public enum TransactionsTypeEnum
        {
            Add = 1,
            Remove = 2,
            Transfere = 4

        }
    }
}
