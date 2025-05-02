using InventoryTracker.Models;
using System.ComponentModel.DataAnnotations.Schema;
using MediatR;
using InventoryTracker.Data;
using InventoryTracker.CQRS.Products.Queries;
using InventoryTracker.CQRS.Warehouse.Queries;
using InventoryTracker.Services;
using InventoryTracker.CQRS.ProductWarehouse.Queries;

namespace InventoryTracker.CQRS.ProductWarehouse.Commands
{
    public class AddProductWarehouseCommand : IRequest<bool>

    {

        public int WarehouseId { get; set; }


        public int ProductID { get; set; }

        public int ProductQuantity { get; set; }
    }

    public class AddProductWarehouseCommandHandler : IRequestHandler<AddProductWarehouseCommand, bool>
    {
        private readonly IGeneralRepo<Models.ProductWarehouse> repo;
        private readonly IMediator mediator;

        public AddProductWarehouseCommandHandler(IGeneralRepo<Models.ProductWarehouse> repo, IMediator mediator)
        {
            this.repo = repo;
            this.mediator = mediator;
        }
        public async Task<bool> Handle(AddProductWarehouseCommand request, CancellationToken cancellationToken)
        {
            var productWarehosue = await mediator.Send(new CheckProductWarehouseByIdQuery() { ProductID = request.ProductID, WarehouseId = request.WarehouseId });
            var product = await mediator.Send(new GetProductByIdQuery() { Id = request.ProductID });
            var warehouse = await mediator.Send(new GetWarehouseByIdNotDeletedQuery() { Id = request.WarehouseId });
            if (product == null || warehouse == null || productWarehosue != null)
            {
                return false;
            }
            Models.ProductWarehouse productWarehouse = request.Map<Models.ProductWarehouse>();
            repo.Add(productWarehouse);

            return true;
        }
    }
}
