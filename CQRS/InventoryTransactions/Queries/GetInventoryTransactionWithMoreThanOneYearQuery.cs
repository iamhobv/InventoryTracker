using InventoryTracker.Data;
using InventoryTracker.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.CQRS.InventoryTransactions.Queries
{
    public class GetInventoryTransactionWithMoreThanOneYearQuery : IRequest<IEnumerable<InventoryTransaction>>
    {

    }
    public class GetInventoryTransactionWithMoreThanOneYearQueryHandler : IRequestHandler<GetInventoryTransactionWithMoreThanOneYearQuery, IEnumerable<InventoryTransaction>>
    {
        private readonly IGeneralRepo<InventoryTransaction> repo;

        public GetInventoryTransactionWithMoreThanOneYearQueryHandler(IGeneralRepo<InventoryTransaction> repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<InventoryTransaction>> Handle(GetInventoryTransactionWithMoreThanOneYearQuery request, CancellationToken cancellationToken)
        {
            DateTime oneYearAgo = DateTime.Now.AddYears(-1);


            return await repo.GetFilter(t => t.IsDeleted == false && t.CreatedDate < oneYearAgo).ToListAsync();
        }
    }

}
