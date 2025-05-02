using InventoryTracker.CQRS.InventoryTransactions.Commands;
using InventoryTracker.CQRS.Products.Commands;
using InventoryTracker.CQRS.ProductWarehouse.Commands;
using InventoryTracker.CQRS.ProductWarehouse.Orchestrators;
using InventoryTracker.CQRS.ProductWarehouse.Queries;
using InventoryTracker.Services;
using MediatR;
using static InventoryTracker.Enums.TransactionTypes;

namespace InventoryTracker.CQRS.InventoryTransactions.Orchestrators
{
    public class AddInventoryOrchesterator : IRequest<bool>
    {
        public string UserId { get; set; }

        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public TransactionsTypeEnum TransactionsType { get; set; }

        public int WarehouseId { get; set; }
    }
    public class AddInventoryOrchesteratorHandler : IRequestHandler<AddInventoryOrchesterator, bool>
    {
        private readonly IMediator mediator;

        public AddInventoryOrchesteratorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<bool> Handle(AddInventoryOrchesterator request, CancellationToken cancellationToken)
        {
            AddInventoryTranactionCommand inventoryTranactionCommand = request.Map<AddInventoryTranactionCommand>();
            var InventoryTranactionResult = await mediator.Send(inventoryTranactionCommand);//3mlna add ll inventory tranasction hlw ? hlw



            var ExistingProdutWarehouseRes = await mediator.Send(new GetProductWarehosueByIdQuery() { ProductID = request.ProductId, WarehouseId = request.WarehouseId });
            if (ExistingProdutWarehouseRes == null)
            {
                var addProdutWarehouseRes = await mediator.Send(new AddWarehousProductOrchestrator() { ProductID = request.ProductId, ProductQuantity = request.ProductId, WarehouseId = request.WarehouseId });
                if (addProdutWarehouseRes)
                {
                    return true;

                }


                return false;

                //var addProdutWarehouseRes = await mediator.Send(request.Map<AddProductWarehouseCommand>());//lw null hn3ml add 3shan hya msh mwgoda

            }
            else
            {


                var newQuanitty = request.Quantity;//+ ExistingProdutWarehouseRes.ProductQuantity;
                UpdateWarehousOrchesterator productWarehouseCommand = new UpdateWarehousOrchesterator()
                {
                    ProductId = request.ProductId,
                    WarehouseId = request.WarehouseId,
                    Quantity = newQuanitty
                };
                var updateProductWarehouseRes = await mediator.Send(productWarehouseCommand);
                return true;

            }

        }
    }

}
