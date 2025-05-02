using InventoryTracker.Data;
using InventoryTracker.Models;
using InventoryTracker.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostAssessment.DTO.ProductDTOs;

namespace InventoryTracker.CQRS.Products.Queries
{

    public class GetProductForUpdateByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }
    }
    public class GetProductForUpdateByIdQueryHandler : IRequestHandler<GetProductForUpdateByIdQuery, Product>
    {
        private readonly IGeneralRepo<Product> repo;

        public GetProductForUpdateByIdQueryHandler(IGeneralRepo<Product> repo)
        {
            this.repo = repo;
        }
        public async Task<Product> Handle(GetProductForUpdateByIdQuery request, CancellationToken cancellationToken)
        {
            return await repo.GetFilter(p => p.ID == request.Id && p.IsDeleted == false).ProjectTo<Product>().FirstOrDefaultAsync();
        }
    }
}
