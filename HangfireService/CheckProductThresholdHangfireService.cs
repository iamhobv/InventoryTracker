using Azure.Core;
using InventoryTracker.CQRS.Products.Queries;
using MediatR;

namespace InventoryTracker.HangfireService
{

    public class CheckProductThresholdHangfireServiceHandler
    {

        private readonly IMediator mediator;

        public CheckProductThresholdHangfireServiceHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task CheckProductThresholdAsync()
        {
            var res = await mediator.Send(new GetProductLessThanThresholdQuery());
            foreach (var item in res)
            {
                await mediator.Send(new CheckProductThreshold() { ProductId = item.Id });

            }
        }


    }
}
