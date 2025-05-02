using InventoryTracker.Data;
using InventoryTracker.Models;
using InventoryTracker.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostAssessment.DTO.ProductDTOs;

namespace InventoryTracker.CQRS.Products.Queries
{

    public class GetProductByIdQuery : IRequest<GetProductDTO>
    {
        public int Id { get; set; }
    }
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductDTO>
    {
        private readonly IGeneralRepo<Product> repo;

        public GetProductByIdQueryHandler(IGeneralRepo<Product> repo)
        {
            this.repo = repo;
        }
        public async Task<GetProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await repo.GetFilter(p => p.ID == request.Id && p.IsDeleted == false).ProjectTo<GetProductDTO>().FirstOrDefaultAsync();
        }
    }
}
