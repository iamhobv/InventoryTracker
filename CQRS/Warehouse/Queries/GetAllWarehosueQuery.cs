using AutoMapper.QueryableExtensions;
using InventoryTracker.Data;
using InventoryTracker.DTOs.WarehouseDTOs;
using InventoryTracker.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.CQRS.Warehouse.Queries
{
    public class GetAllWarehosueQuery : IRequest<IEnumerable<GetWarehouseDTO>>
    {
    }
    public class GetAllWarehosueQueryHandler : IRequestHandler<GetAllWarehosueQuery, IEnumerable<GetWarehouseDTO>>
    {
        private readonly IGeneralRepo<Models.Warehouse> repo;

        public GetAllWarehosueQueryHandler(IGeneralRepo<Models.Warehouse> repo)
        {
            this.repo = repo;
        }
        public async Task<IEnumerable<GetWarehouseDTO>> Handle(GetAllWarehosueQuery request, CancellationToken cancellationToken)
        {
            return await repo.GetFilter(w => w.IsDeleted == false).ProjectTo<GetWarehouseDTO>().ToListAsync();
        }
    }
}
