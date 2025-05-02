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
    public class GetWarehouseByIdNotDeletedQuery : IRequest<GetWarehouseDTO>
    {
        public int Id { get; set; }
    }






    public class GetWarehouseByIdNotDeletedQueryHandler : IRequestHandler<GetWarehouseByIdNotDeletedQuery, GetWarehouseDTO>
    {
        private readonly IGeneralRepo<Models.Warehouse> warehouseRepo;

        public GetWarehouseByIdNotDeletedQueryHandler(IGeneralRepo<Models.Warehouse> warehouseRepo)
        {
            this.warehouseRepo = warehouseRepo;
        }
        public async Task<GetWarehouseDTO> Handle(GetWarehouseByIdNotDeletedQuery request, CancellationToken cancellationToken)
        {
            return await warehouseRepo.GetFilter(x => x.ID == request.Id && x.IsDeleted == false).ProjectTo<GetWarehouseDTO>().FirstOrDefaultAsync();
        }

    }


}
