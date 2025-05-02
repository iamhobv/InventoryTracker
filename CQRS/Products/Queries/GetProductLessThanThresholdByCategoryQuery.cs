using InventoryTracker.Data;
using InventoryTracker.Models;
using InventoryTracker.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostAssessment.DTO.ProductDTOs;

namespace InventoryTracker.CQRS.Products.Queries
{
    public class GetProductLessThanThresholdByCategoryQuery : IRequest<IEnumerable<GetProductDTO>>
    {
        public int CategoryId { get; set; }
    }
    public class GetProductLessThanThresholdByCategoryQueryHandler : IRequestHandler<GetProductLessThanThresholdByCategoryQuery, IEnumerable<GetProductDTO>>
    {
        private readonly IGeneralRepo<Product> repo;

        public GetProductLessThanThresholdByCategoryQueryHandler(IGeneralRepo<Product> repo)
        {
            this.repo = repo;
        }
        public async Task<IEnumerable<GetProductDTO>> Handle(GetProductLessThanThresholdByCategoryQuery request, CancellationToken cancellationToken)
        {
            return await repo.GetFilter(p => p.IsDeleted == false && p.CategoryId == request.CategoryId && p.LowStockThreshold > p.Quantity).ProjectTo<GetProductDTO>().ToListAsync();
        }
    }
}
