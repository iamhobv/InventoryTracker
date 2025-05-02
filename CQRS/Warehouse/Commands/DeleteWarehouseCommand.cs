using InventoryTracker.CQRS.Warehouse.Queries;
using InventoryTracker.Data;
using InventoryTracker.DTOs.WarehouseDTOs;
using MediatR;

namespace InventoryTracker.CQRS.Warehouse.Commands
{
    public class DeleteWarehouseCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteWarehouseCommandHandler : IRequestHandler<DeleteWarehouseCommand, bool>
    {
        private readonly IGeneralRepo<Models.Warehouse> repo;
        private readonly IMediator mediator;

        public DeleteWarehouseCommandHandler(IGeneralRepo<Models.Warehouse> repo, IMediator mediator)
        {
            this.repo = repo;
            this.mediator = mediator;
        }
        public async Task<bool> Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
        {
            GetWarehouseDTO existingWarehouse = await mediator.Send(new GetWarehouseByIdNotDeletedQuery() { Id = request.Id });
            if (existingWarehouse == null)
            {
                return false;
            }
            repo.Remove(request.Id);
            return true;
        }
    }
}
