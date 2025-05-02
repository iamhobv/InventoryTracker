using InventoryTracker.CQRS;
using InventoryTracker.CQRS.ArchivedInventoryTransaction.Commands;
using InventoryTracker.CQRS.InventoryTransactions.Commands;
using InventoryTracker.CQRS.InventoryTransactions.Queries;
using MediatR;

namespace InventoryTracker.HangfireService
{
    public class CheckProductYearArchivedTransactionHangfireService
    {
        private readonly IMediator mediator;

        public CheckProductYearArchivedTransactionHangfireService(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task CheckProductYearAsync()
        {
            try
            {
                var res = await mediator.Send(new GetInventoryTransactionWithMoreThanOneYearQuery());

                foreach (var item in res)
                {
                    var addResult = await mediator.Send(new AddArchivedInventoryTrasactionCommand() { InventoryTransaction = item });
                    if (addResult)
                    {
                        await mediator.Send(new SaveChanges());
                        var deleteResult = await mediator.Send(new DeleteInventoryTransactionCommad() { Id = item.ID });
                        if (deleteResult)
                        {
                            await mediator.Send(new SaveChanges());
                        }
                    }

                }
            }
            catch (Exception e)
            {

                throw e;
            }

        }
    }
}
