using InventoryTracker.Data;
using InventoryTracker.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.CQRS.InventoryTransactions.Queries
{
    public class GetInventoryTransactionByIdQuery : IRequest<InventoryTransaction>
    {
        public int ID { get; set; }
    }
    public class GetInventoryTransactionByIdQueryHandler : IRequestHandler<GetInventoryTransactionByIdQuery, InventoryTransaction>
    {
        private readonly IGeneralRepo<InventoryTransaction> repo;

        public GetInventoryTransactionByIdQueryHandler(IGeneralRepo<InventoryTransaction> repo)
        {
            this.repo = repo;
        }
        public async Task<InventoryTransaction> Handle(GetInventoryTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            return await repo.GetFilter(i => i.IsDeleted == false && i.ID == request.ID).FirstOrDefaultAsync();
        }
    }
}
