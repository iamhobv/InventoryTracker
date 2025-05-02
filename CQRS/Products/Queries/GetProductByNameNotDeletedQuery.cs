using InventoryTracker.Data;
using InventoryTracker.Models;
using InventoryTracker.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostAssessment.DTO.ProductDTOs;

namespace InventoryTracker.CQRS.Products.Queries
{
    public class GetProductByNameNotDeletedQuery : IRequest<GetProductDTO>
    {
        public string Name { get; set; }
    }
    public class GetProductByNameNotDeletedHandler : IRequestHandler<GetProductByNameNotDeletedQuery, GetProductDTO>
    {
        private readonly IGeneralRepo<Product> repo;

        public GetProductByNameNotDeletedHandler(IGeneralRepo<Product> repo)
        {
            this.repo = repo;
        }
        public async Task<GetProductDTO> Handle(GetProductByNameNotDeletedQuery request, CancellationToken cancellationToken)
        {
            return await repo.GetFilter(p => p.Name.Equals(request.Name) && p.IsDeleted == false).ProjectTo<GetProductDTO>().FirstOrDefaultAsync();
        }
    }

}

