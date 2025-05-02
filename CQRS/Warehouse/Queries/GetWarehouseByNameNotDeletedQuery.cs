using System.Security.Principal;
using InventoryTracker.CQRS.Category.Queries;
using InventoryTracker.Data;
using InventoryTracker.DTOs.WarehouseDTOs;
using InventoryTracker.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostAssessment.DTO.CategoryDTOs;

namespace InventoryTracker.CQRS.Warehouse.Queries
{
    public class GetWarehouseByNameNotDeletedQuery : IRequest<GetWarehouseDTO>
    {
        public string Name { get; set; }
    }






    public class GetWarehouseByNameNotDeletedQueryHandler : IRequestHandler<GetWarehouseByNameNotDeletedQuery, GetWarehouseDTO>
    {
        private readonly IGeneralRepo<Models.Warehouse> warehouseRepo;

        public GetWarehouseByNameNotDeletedQueryHandler(IGeneralRepo<Models.Warehouse> warehouseRepo)
        {
            this.warehouseRepo = warehouseRepo;
        }
        public async Task<GetWarehouseDTO> Handle(GetWarehouseByNameNotDeletedQuery request, CancellationToken cancellationToken)
        {
            return warehouseRepo.GetFilter(x => x.Name.Equals(request.Name)).ProjectTo<GetWarehouseDTO>().FirstOrDefault();
        }

    }


}
