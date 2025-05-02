using InventoryTracker.CQRS.Products.Commands;
using InventoryTracker.CQRS.ProductWarehouse.Commands;
using InventoryTracker.Data;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;

namespace InventoryTracker.CQRS.ProductWarehouse.Orchestrators
{
    public class UpdateWarehousOrchesterator : IRequest<bool>
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
    }

    public class UpdateWarehousOrchesteratorHandler : IRequestHandler<UpdateWarehousOrchesterator, bool>
    {
        private readonly IGeneralRepo<Models.ProductWarehouse> repo;
        private readonly IMediator mediator;

        public UpdateWarehousOrchesteratorHandler(IGeneralRepo<Models.ProductWarehouse> repo, IMediator mediator)
        {
            this.repo = repo;
            this.mediator = mediator;
        }

        public async Task<bool> Handle(UpdateWarehousOrchesterator request, CancellationToken cancellationToken)
        {
            var updateProductWarehouse = await mediator.Send(new UpdateProductWarehouseCommand() { ProductId = request.ProductId, Quantity = request.Quantity, WarehouseId = request.WarehouseId });
            var updateProductQuantity = await mediator.Send(new UpdateProductQuantityCommand() { Id = request.ProductId, Quantity = request.Quantity });
            if (updateProductWarehouse && updateProductWarehouse)
            {
                return true;

            }
            return false;
        }
    }
}
