using InventoryTracker.Data;
using InventoryTracker.DTOs.ProductWarehouseDTOs;
using InventoryTracker.DTOs.WarehouseDTOs;
using InventoryTracker.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.CQRS.ProductWarehouse.Queries
{
    public class CheckProductWarehouseByIdQuery : IRequest<CheckProductWarehouseByIdDTO>
    {
        public int WarehouseId { get; set; }


        public int ProductID { get; set; }
    }

    public class CheckProductWarehouseByIdQueryHandler : IRequestHandler<CheckProductWarehouseByIdQuery, CheckProductWarehouseByIdDTO>
    {
        private readonly IGeneralRepo<Models.ProductWarehouse> repo;

        public CheckProductWarehouseByIdQueryHandler(IGeneralRepo<Models.ProductWarehouse> repo)
        {
            this.repo = repo;
        }
        public async Task<CheckProductWarehouseByIdDTO> Handle(CheckProductWarehouseByIdQuery request, CancellationToken cancellationToken)
        {
            return await repo.GetFilter(pw => pw.ProductID == request.ProductID && pw.WarehouseId == request.WarehouseId && pw.IsDeleted == false).ProjectTo<CheckProductWarehouseByIdDTO>().FirstOrDefaultAsync();
        }
    }
}
