using System.ComponentModel.DataAnnotations;
using InventoryTracker.CQRS.Products.Commands;
using InventoryTracker.CQRS.Products.Queries;
using InventoryTracker.CQRS.ProductWarehouse.Commands;
using MediatR;

namespace InventoryTracker.CQRS.ProductWarehouse.Orchestrators
{
    public class AddWarehousProductOrchestrator : IRequest<bool>
    {
        public int WarehouseId { get; set; }


        public int ProductID { get; set; }

        public int ProductQuantity { get; set; }
    }

    public class AddWarehousProductOrchestratorHandler : IRequestHandler<AddWarehousProductOrchestrator, bool>
    {
        private readonly IMediator mediator;

        public AddWarehousProductOrchestratorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<bool> Handle(AddWarehousProductOrchestrator request, CancellationToken cancellationToken)
        {
            var AddProductWarehouse = await mediator.Send(new AddProductWarehouseCommand() { ProductID = request.ProductID, ProductQuantity = request.ProductQuantity, WarehouseId = request.WarehouseId });
            var updateProductQuantity = await mediator.Send(new UpdateProductQuantityCommand() { Id = request.ProductID, Quantity = request.ProductQuantity });
            if (AddProductWarehouse && updateProductQuantity)
            {
                return true;
            }
            return false;
        }
    }

}
