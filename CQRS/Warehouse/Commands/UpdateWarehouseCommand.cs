using InventoryTracker.CQRS.Warehouse.Queries;
using InventoryTracker.Data;
using InventoryTracker.DTOs.WarehouseDTOs;
using InventoryTracker.Models;
using InventoryTracker.Services;
using MediatR;

namespace InventoryTracker.CQRS.Warehouse.Commands
{
    public class UpdateWarehouseCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }

    public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand, bool>
    {
        private readonly IGeneralRepo<Models.Warehouse> repo;
        private readonly IMediator mediator;


        public UpdateWarehouseCommandHandler(IGeneralRepo<Models.Warehouse> repo, IMediator mediator)
        {
            this.repo = repo;
            this.mediator = mediator;
        }
        public async Task<bool> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            GetWarehouseDTO res = await mediator.Send(new GetWarehouseByIdNotDeletedQuery() { Id = request.Id });
            if (res == null)
            {
                return false;
            }

            res.Location = request.Location ?? res.Location;
            res.Name = request.Name ?? res.Name;
            repo.Update(res.Map<Models.Warehouse>());
            return true;
        }
    }
}
