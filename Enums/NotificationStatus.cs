namespace InventoryTracker.Enums
{
    public class NotificationStatus
    {
        [Flags]

        public enum NotificationStatusEnum
        {
            Read = 1,
            Pending = 2,
        }
    }
}
