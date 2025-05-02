using InventoryTracker.CQRS.ProductWarehouse.Queries;
using InventoryTracker.Data;
using MediatR;

namespace InventoryTracker.CQRS.ProductWarehouse.Commands
{
    public class DeleteProductWarehouseCommand : IRequest<bool>
    {
        public int WarehouseId { get; set; }


        public int ProductID { get; set; }
    }
    public class DeleteProductWarehouseCommandHandler : IRequestHandler<DeleteProductWarehouseCommand, bool>
    {
        private readonly IMediator mediator;
        private readonly IGeneralRepo<Models.ProductWarehouse> repo;

        public DeleteProductWarehouseCommandHandler(IMediator mediator, IGeneralRepo<Models.ProductWarehouse> repo)
        {
            this.mediator = mediator;
            this.repo = repo;
        }
        public async Task<bool> Handle(DeleteProductWarehouseCommand request, CancellationToken cancellationToken)
        {
            var res = await mediator.Send(new GetProductWarehosueByIdQuery() { ProductID = request.ProductID, WarehouseId = request.WarehouseId });
            if (res == null)
            {
                return false;
            }
            repo.Delete(res);
            return true;
        }
    }
}
