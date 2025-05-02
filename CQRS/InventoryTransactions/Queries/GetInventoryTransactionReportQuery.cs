using InventoryTracker.Models;
using System.Linq.Expressions;
using MediatR;
using RoboostAssessment.DTO.TransactionDTOs;
using InventoryTracker.Data;
using Microsoft.EntityFrameworkCore;
using InventoryTracker.Services;

namespace InventoryTracker.CQRS.InventoryTransactions.Queries
{
    public class GetInventoryTransactionReportQuery : IRequest<IEnumerable<GetTTansactionToReportsDTO>>
    {
        public Expression<Func<InventoryTransaction, bool>> filters { get; set; }
        //List<Expression<Func<InventoryTransaction, bool>>> filters
    }

    public class GetInventoryTransactionReportQueryHandler : IRequestHandler<GetInventoryTransactionReportQuery, IEnumerable<GetTTansactionToReportsDTO>>
    {
        private readonly IGeneralRepo<InventoryTransaction> repo;

        public GetInventoryTransactionReportQueryHandler(IGeneralRepo<InventoryTransaction> repo)
        {
            this.repo = repo;
        }
        public async Task<IEnumerable<GetTTansactionToReportsDTO>> Handle(GetInventoryTransactionReportQuery request, CancellationToken cancellationToken)
        {

            return await repo.GetFilter(request.filters).ProjectTo<GetTTansactionToReportsDTO>().ToListAsync();
        }
    }
}
