using InventoryTracker.Data;
using InventoryTracker.Models;
using InventoryTracker.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostAssessment.DTO.ProductDTOs;

namespace InventoryTracker.CQRS.Products.Queries
{
    public class GetProductLessThanThresholdQuery : IRequest<IEnumerable<GetProductDTO>>
    {
    }


    public class GetProductLessThanThresholdQueryHandler : IRequestHandler<GetProductLessThanThresholdQuery, IEnumerable<GetProductDTO>>
    {
        private readonly IGeneralRepo<Product> repo;

        public GetProductLessThanThresholdQueryHandler(IGeneralRepo<Product> repo)
        {
            this.repo = repo;
        }
        public async Task<IEnumerable<GetProductDTO>> Handle(GetProductLessThanThresholdQuery request, CancellationToken cancellationToken)
        {
            return await repo.GetFilter(p => p.IsDeleted == false && p.LowStockThreshold > p.Quantity).ProjectTo<GetProductDTO>().ToListAsync();
        }
    }
}
