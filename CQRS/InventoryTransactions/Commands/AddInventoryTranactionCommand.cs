using System.ComponentModel.DataAnnotations;
using InventoryTracker.CQRS.Products.Queries;
using InventoryTracker.CQRS.Warehouse.Queries;
using InventoryTracker.Data;
using InventoryTracker.Models;
using MediatR;
using static InventoryTracker.Enums.TransactionTypes;

namespace InventoryTracker.CQRS.InventoryTransactions.Commands
{
    public class AddInventoryTranactionCommand : IRequest<bool>
    {
        public string UserId { get; set; }

        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public TransactionsTypeEnum TransactionsType { get; set; }

        public int WarehouseId { get; set; }
    }
    public class AddInventoryTranactionCommandHandler : IRequestHandler<AddInventoryTranactionCommand, bool>
    {
        private readonly IGeneralRepo<InventoryTransaction> repo;
        private readonly IMediator mediator;

        public AddInventoryTranactionCommandHandler(IGeneralRepo<InventoryTransaction> repo, IMediator mediator)
        {
            this.repo = repo;
            this.mediator = mediator;
        }
        public async Task<bool> Handle(AddInventoryTranactionCommand request, CancellationToken cancellationToken)
        {
            var product = await mediator.Send(new GetProductByIdQuery() { Id = request.ProductId });
            var warehouse = await mediator.Send(new GetWarehouseByIdNotDeletedQuery() { Id = request.WarehouseId });
            if (product == null || warehouse == null)
            {
                return false;
            }

            InventoryTransaction inventoryTransaction = new InventoryTransaction()
            {
                CreatedDate = DateTime.Now,
                IsDeleted = false,
                ProductID = request.ProductId,
                Quantity = request.Quantity,
                TransactionsType = request.TransactionsType,
                UserId = request.UserId,
                WarehouseId = request.WarehouseId
            };
            repo.Add(inventoryTransaction);
            return true;
        }
    }

}
