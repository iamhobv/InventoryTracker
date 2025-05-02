using MediatR;

namespace InventoryTracker.CQRS.Products.Events
{
    public class ProductQuantityLessThanThresholdEvent : INotification
    {
        public int ProductID { get; set; }
    }
}
