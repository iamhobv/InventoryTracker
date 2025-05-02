using InventoryTracker.CQRS.InventoryTransactions.Queries;
using InventoryTracker.Enums;
using InventoryTracker.Models;
using MediatR;
using RoboostAssessment.DTO.TransactionDTOs;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static InventoryTracker.Enums.TransactionTypes;

namespace InventoryTracker.Services
{
    public class TransactionHistoryReportBuilder : ITransactionHistoryReportBuilder
    {
        private readonly List<Expression<Func<InventoryTransaction, bool>>> filters;
        private readonly IMediator mediator;

        public TransactionHistoryReportBuilder(IMediator mediator)
        {
            this.filters = new List<Expression<Func<InventoryTransaction, bool>>>();
            this.mediator = mediator;
        }
        public ITransactionHistoryReportBuilder SetCategory(int? categoryId)
        {
            if (categoryId > 0)
            {
                filters.Add(t => t.Product.CategoryId == categoryId);
            }
            return this;
        }
        public ITransactionHistoryReportBuilder SetTransactionsType(TransactionsTypeEnum? transactionsType)
        {
            if (transactionsType != null)
            {
                filters.Add(t => t.TransactionsType == transactionsType);

            }
            return this;

        }

        public ITransactionHistoryReportBuilder SetDateRange(DateTime? startDate, DateTime? endDate)
        {
            if (startDate.HasValue)
            {
                filters.Add(t => t.CreatedDate >= startDate);
            }
            if (endDate.HasValue)
            {
                filters.Add(t => t.CreatedDate <= endDate);
            }
            return this;
        }

        public ITransactionHistoryReportBuilder SetProduct(int? productId)
        {
            if (productId > 0)
            {
                filters.Add(t => t.ProductID == productId);
            }
            return this;
        }




        public async Task<IEnumerable<GetTTansactionToReportsDTO>> Build()
        {
            List<GetTTansactionToReportsDTO> data;
            if (filters.Count == 0)
            {

                data = new List<GetTTansactionToReportsDTO>();
                //data = inventoryTransactionService.GetReportFilter(t => false);
            }
            else
            {
                Expression<Func<InventoryTransaction, bool>> combinedFilter = filters.Aggregate((current, next) =>
            Expression.Lambda<Func<InventoryTransaction, bool>>(Expression.AndAlso(current.Body, Expression.Invoke(next, current.Parameters)), current.Parameters));

                data = (List<GetTTansactionToReportsDTO>)await mediator.Send(new GetInventoryTransactionReportQuery() { filters = combinedFilter });
            }


            return data;
        }

        //public ITransactionHistoryReportBuilder SetTransactionsType(TransactionsTypeEnum? transactionsType)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
