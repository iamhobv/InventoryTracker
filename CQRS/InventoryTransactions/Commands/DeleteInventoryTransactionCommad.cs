using InventoryTracker.CQRS.InventoryTransactions.Queries;
using InventoryTracker.Data;
using InventoryTracker.Models;
using MediatR;

namespace InventoryTracker.CQRS.InventoryTransactions.Commands
{
    public class DeleteInventoryTransactionCommad : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeleteInventoryTransactionCommadHandler : IRequestHandler<DeleteInventoryTransactionCommad, bool>
    {
        private readonly IMediator mediator;
        private readonly IGeneralRepo<InventoryTransaction> repo;

        public DeleteInventoryTransactionCommadHandler(IMediator mediator, IGeneralRepo<InventoryTransaction> repo)
        {
            this.mediator = mediator;
            this.repo = repo;
        }
        public async Task<bool> Handle(DeleteInventoryTransactionCommad request, CancellationToken cancellationToken)
        {
            var res = await mediator.Send(new GetInventoryTransactionByIdQuery() { ID = request.Id });
            if (res == null)
            {
                return false;
            }
            repo.Remove(request.Id);
            return true;

        }
    }
}
