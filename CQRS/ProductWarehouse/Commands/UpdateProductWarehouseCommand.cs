using InventoryTracker.CQRS.ProductWarehouse.Queries;
using InventoryTracker.Data;
using InventoryTracker.DTOs.ProductWarehouseDTOs;
using MediatR;

namespace InventoryTracker.CQRS.ProductWarehouse.Commands
{
    public class UpdateProductWarehouseCommand : IRequest<bool>
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }

    }

    public class UpdateProductWarehouseCommandHandler : IRequestHandler<UpdateProductWarehouseCommand, bool>
    {
        private readonly IGeneralRepo<Models.ProductWarehouse> repo;
        private readonly IMediator mediator;

        public UpdateProductWarehouseCommandHandler(IGeneralRepo<Models.ProductWarehouse> repo, IMediator mediator)
        {
            this.repo = repo;
            this.mediator = mediator;
        }
        public async Task<bool> Handle(UpdateProductWarehouseCommand request, CancellationToken cancellationToken)
        {
            var checkProductWarehouse = await mediator.Send(new GetProductWarehosueByIdQuery() { ProductID = request.ProductId, WarehouseId = request.WarehouseId });
            if (checkProductWarehouse == null)
            {
                return false;
            }
            if (request.Quantity < 0 && -request.Quantity > checkProductWarehouse.ProductQuantity)
            {
                checkProductWarehouse.ProductQuantity = 0;
            }
            else
            {
                checkProductWarehouse.ProductQuantity = request.Quantity + checkProductWarehouse.ProductQuantity;
            }

            repo.Update(checkProductWarehouse);
            return true;
        }
    }
}
