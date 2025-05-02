using System.Diagnostics.CodeAnalysis;
using InventoryTracker.CQRS.InventoryTransactions.Commands;
using InventoryTracker.CQRS.Products.Commands;
using InventoryTracker.CQRS.Products.Events;
using InventoryTracker.CQRS.Products.Queries;
using InventoryTracker.CQRS.ProductWarehouse.Commands;
using InventoryTracker.CQRS.ProductWarehouse.Orchestrators;
using InventoryTracker.CQRS.ProductWarehouse.Queries;
using InventoryTracker.Models;
using InventoryTracker.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static InventoryTracker.Enums.TransactionTypes;

namespace InventoryTracker.CQRS.InventoryTransactions.Orchestrators
{
    public class RemoveInventoryOrchesterator : IRequest<bool>
    {
        public string UserId { get; set; }

        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public TransactionsTypeEnum TransactionsType { get; set; }

    }
    public class RemoveInventoryOrchesteratorHandler : IRequestHandler<RemoveInventoryOrchesterator, bool>
    {
        private readonly IMediator mediator;

        public RemoveInventoryOrchesteratorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<bool> Handle(RemoveInventoryOrchesterator request, CancellationToken cancellationToken)
        {
            AddInventoryTranactionCommand inventoryTranactionCommand = request.Map<AddInventoryTranactionCommand>();
            var InventoryTranactionResult = await mediator.Send(inventoryTranactionCommand);//3mlna add ll inventory tranasction hlw ? hlw





            var ExistingProdutWarehouseRes =
                await mediator.Send(new GetProductWarehosueByIdQuery()
                {
                    ProductID = request.ProductId,
                    WarehouseId = request.WarehouseId
                });//bashof el product eza kan mwgod fl warehouse da wla la

            if (ExistingProdutWarehouseRes == null)
            {
                return false;
            }
            else
            {
                var newQuanitty = -request.Quantity;
                UpdateWarehousOrchesterator productWarehouseCommand = new UpdateWarehousOrchesterator()
                {
                    ProductId = request.ProductId,
                    WarehouseId = request.WarehouseId,
                    Quantity = newQuanitty
                };

                var updateProductWarehouseRes = await mediator.Send(productWarehouseCommand);//bgeb el prod b3d el t3del w bcheck eza el Quantity<LowStockThreshold

                if (updateProductWarehouseRes)
                {

                    await mediator.Send(new SaveChanges());
                    await mediator.Send(new CheckProductThreshold() {ProductId= request.ProductId });

                    //var Product = await mediator.Send(new GetProductByIdQuery() { Id = request.ProductId });

                    //if (Product.Quantity < Product.LowStockThreshold)
                    //{
                    //    await mediator.Publish(new ProductQuantityLessThanThresholdEvent() { ProductID = request.ProductId });
                    //}
                }



                return true;

            }

        }
    }

}
