using InventoryTracker.Data;
using InventoryTracker.DTOs.ProductWarehouseDTOs;
using InventoryTracker.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.CQRS.ProductWarehouse.Queries
{
    public class GetProductWarehouseByPRoductIdQuery : IRequest<IEnumerable<GetProductWarehouseProductsDTO>>
    {
        public int WarehouseId { get; set; }
    }
    public class GetProductWarehouseByPRoductIdQueryHandler : IRequestHandler<GetProductWarehouseByPRoductIdQuery, IEnumerable<GetProductWarehouseProductsDTO>>
    {
        private readonly IGeneralRepo<Models.ProductWarehouse> repo;

        public GetProductWarehouseByPRoductIdQueryHandler(IGeneralRepo<Models.ProductWarehouse> repo)
        {
            this.repo = repo;
        }

        public async Task<IEnumerable<GetProductWarehouseProductsDTO>> Handle(GetProductWarehouseByPRoductIdQuery request, CancellationToken cancellationToken)
        {
            return await repo.GetFilter(pw => pw.WarehouseId == request.WarehouseId && pw.IsDeleted == false).ProjectTo<GetProductWarehouseProductsDTO>().ToListAsync();
        }
    }
}
