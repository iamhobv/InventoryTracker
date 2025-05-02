using InventoryTracker.CQRS.Products.Queries;
using InventoryTracker.CQRS.ProductWarehouse.Commands;
using InventoryTracker.CQRS.ProductWarehouse.Queries;
using InventoryTracker.CQRS.Warehouse.Queries;
using InventoryTracker.Data;
using InventoryTracker.Models;
using MediatR;

namespace InventoryTracker.CQRS.InventoryTransactions.Commands
{
    public class TransfereInventoryCommand : IRequest<bool>
    {
        public int OldWarehouseID { get; set; }
        public int NewWarehouseID { get; set; }
        public int ProductID { get; set; }
        public string UserID { get; set; }
    }

    public class TransfereInventoryCommandHandler : IRequestHandler<TransfereInventoryCommand, bool>
    {
        private readonly IMediator mediator;
        private readonly IGeneralRepo<Models.ProductWarehouse> repo;

        public TransfereInventoryCommandHandler(IMediator mediator, IGeneralRepo<Models.ProductWarehouse> repo)
        {
            this.mediator = mediator;
            this.repo = repo;
        }
        public async Task<bool> Handle(TransfereInventoryCommand request, CancellationToken cancellationToken)
        {
            var product = await mediator.Send(new GetProductByIdQuery() { Id = request.ProductID });
            var warehouse = await mediator.Send(new GetWarehouseByIdNotDeletedQuery() { Id = request.NewWarehouseID });
            if (product == null || warehouse == null)
            {
                return false;
            }

            var productWarehouse = await mediator.Send(new GetProductWarehosueByIdQuery()
            {
                ProductID = request.ProductID,
                WarehouseId = request.NewWarehouseID
            });

            if (productWarehouse == null)
            {
                var prodWarehous = await mediator.Send(new AddProductWarehouseCommand() { ProductID = request.ProductID, WarehouseId = request.NewWarehouseID, ProductQuantity = product.Quantity });
                if (prodWarehous)
                {

                    var inventoryTransaction = await mediator.Send(new AddInventoryTranactionCommand()
                    {
                        ProductId = request.ProductID,
                        Quantity = product.Quantity,
                        TransactionsType = Enums.TransactionTypes.TransactionsTypeEnum.Transfere,
                        UserId = request.UserID,
                        WarehouseId = request.NewWarehouseID

                    });
                    if (inventoryTransaction)
                    {
                        //delete
                        var delProdWarehous = await mediator.Send(new DeleteProductWarehouseCommand() { ProductID = request.ProductID, WarehouseId = request.OldWarehouseID });
                        if (delProdWarehous)
                            return true;
                    }
                }

            }
            else
            {
                var OldProductWarehouse = await mediator.Send(new GetProductWarehosueByIdQuery()
                {
                    ProductID = request.ProductID,
                    WarehouseId = request.OldWarehouseID
                });
                var podWarehouse = await mediator.Send(new UpdateProductWarehouseCommand()
                {
                    ProductId = request.ProductID,
                    WarehouseId = request.NewWarehouseID,
                    Quantity = OldProductWarehouse.ProductQuantity
                });
                if (podWarehouse)
                {
                    var inventoryTransaction = await mediator.Send(new AddInventoryTranactionCommand()
                    {
                        ProductId = request.ProductID,
                        Quantity = product.Quantity,
                        TransactionsType = Enums.TransactionTypes.TransactionsTypeEnum.Transfere,
                        UserId = request.UserID,
                        WarehouseId = request.NewWarehouseID

                    });
                    if (inventoryTransaction)
                    {
                        //delete
                        var delProdWarehous = await mediator.Send(new DeleteProductWarehouseCommand() { ProductID = request.ProductID, WarehouseId = request.OldWarehouseID });
                        if (delProdWarehous)
                            return true;
                    }
                }


            }
            return false;
            //InventoryTransaction inventoryTransaction = new InventoryTransaction()
            //{
            //    CreatedDate = DateTime.Now,
            //    IsDeleted = false,
            //    ProductID = request.ProductID,
            //    Quantity = request.Quantity,
            //    TransactionsType = request.TransactionsType,
            //    UserId = request.UserID,
            //    WarehouseId = request.WarehouseId
            //};
        }
    }
}
