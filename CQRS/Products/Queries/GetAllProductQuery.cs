using InventoryTracker.Data;
using InventoryTracker.Models;
using InventoryTracker.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostAssessment.DTO.ProductDTOs;

namespace InventoryTracker.CQRS.Products.Queries
{
    public class GetAllProductQuery : IRequest<IEnumerable<GetProductDTO>>
    {
    }
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IEnumerable<GetProductDTO>>
    {
        private readonly IGeneralRepo<Product> repo;

        public GetAllProductQueryHandler(IGeneralRepo<Product> repo)
        {
            this.repo = repo;
        }
        public async Task<IEnumerable<GetProductDTO>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            return await repo.GetFilter(p => p.IsDeleted == false).ProjectTo<GetProductDTO>().ToListAsync();
        }
    }

}
