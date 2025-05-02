using InventoryTracker.CQRS.Categories.Queries;
using InventoryTracker.CQRS.Warehouse.Queries;
using InventoryTracker.Data;
using InventoryTracker.DTOs.WarehouseDTOs;
using InventoryTracker.Services;
using MediatR;

namespace InventoryTracker.CQRS.Warehouse.Commands
{
    public class AddWarehouseCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Location { get; set; }
    }
    public class AddWarehouseCommandHandler : IRequestHandler<AddWarehouseCommand, bool>
    {
        private readonly IGeneralRepo<Models.Warehouse> warehouseRepo;
        private readonly IMediator mediator;

        public AddWarehouseCommandHandler(IGeneralRepo<Models.Warehouse> warehouseRepo, IMediator mediator)
        {
            this.warehouseRepo = warehouseRepo;
            this.mediator = mediator;
        }
        public async Task<bool> Handle(AddWarehouseCommand request, CancellationToken cancellationToken)
        {
            var existWarehosue = await mediator.Send(new GetWarehouseByNameNotDeletedQuery() { Name = request.Name });
            if (existWarehosue != null)
            {
                return false;
            }

            Models.Warehouse warehouse = request.Map<Models.Warehouse>();
            warehouse.IsDeleted = false;
            warehouseRepo.Add(warehouse);
            return true;

        }
    }
}

