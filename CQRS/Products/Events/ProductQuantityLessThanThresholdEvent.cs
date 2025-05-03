using MediatR;

namespace InventoryTracker.CQRS.Products.Events
{
    public class ProductQuantityLessThanThresholdEvent : INotification
    {
        public int ProductID { get; set; }
        public string ProdName { get; set; }
    }
}
