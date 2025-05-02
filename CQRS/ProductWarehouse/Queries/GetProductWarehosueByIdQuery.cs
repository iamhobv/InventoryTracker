using InventoryTracker.Data;
using InventoryTracker.DTOs.ProductWarehouseDTOs;
using InventoryTracker.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.CQRS.ProductWarehouse.Queries
{
    public class GetProductWarehosueByIdQuery : IRequest<Models.ProductWarehouse>
    {
        public int WarehouseId { get; set; }


        public int ProductID { get; set; }
    }
    public class GetProductWarehosueByIdQueryHandler : IRequestHandler<GetProductWarehosueByIdQuery, Models.ProductWarehouse>
    {
        private readonly IGeneralRepo<Models.ProductWarehouse> repo;

        public GetProductWarehosueByIdQueryHandler(IGeneralRepo<Models.ProductWarehouse> repo)
        {
            this.repo = repo;
        }

        public async Task<Models.ProductWarehouse> Handle(GetProductWarehosueByIdQuery request, CancellationToken cancellationToken)
        {
            return await repo.GetFilter(pw => pw.ProductID == request.ProductID && pw.WarehouseId == request.WarehouseId && pw.IsDeleted == false).FirstOrDefaultAsync();
        }
    }


}
