using InventoryTracker.Data;
using InventoryTracker.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostAssessment.DTO.ProductDTOs;

namespace InventoryTracker.CQRS.Products.Queries
{
    public class GetProductByIDForQuantityUpdateQuery : IRequest<Product>

    {
        public int Id { get; set; }
    }

    public class GetProductByIDForQuantityUpdateQueryHandler : IRequestHandler<GetProductByIDForQuantityUpdateQuery, Product>
    {
        private readonly IGeneralRepo<Product> repo;

        public GetProductByIDForQuantityUpdateQueryHandler(IGeneralRepo<Product> repo)
        {
            this.repo = repo;
        }
        public async Task<Product> Handle(GetProductByIDForQuantityUpdateQuery request, CancellationToken cancellationToken)
        {
            return await repo.GetFilter(p => p.ID == request.Id && p.IsDeleted == false).FirstOrDefaultAsync();
        }
    }
}
