using InventoryTracker.CQRS.Products.Events;
using MediatR;

namespace InventoryTracker.CQRS.Products.Queries
{
    public class CheckProductThreshold : IRequest<bool>
    {
        public int ProductId { get; set; }
    }

    public class CheckProductThresholdHandler : IRequestHandler<CheckProductThreshold, bool>
    {
        private readonly IMediator mediator;

        public CheckProductThresholdHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<bool> Handle(CheckProductThreshold request, CancellationToken cancellationToken)
        {
            var Product = await mediator.Send(new GetProductByIdQuery() { Id = request.ProductId });

            if (Product.Quantity < Product.LowStockThreshold)
            {
                await mediator.Publish(new ProductQuantityLessThanThresholdEvent() { ProductID = request.ProductId, ProdName = Product.Name });
                return true;
            }
            return false;
        }
    }
}
