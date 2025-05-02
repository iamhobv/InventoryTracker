using System.Collections.Generic;
using InventoryTracker.CQRS.Categories.Queries;
using InventoryTracker.Data;
using InventoryTracker.Models;
using InventoryTracker.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RoboostAssessment.DTO.ProductDTOs;

namespace InventoryTracker.CQRS.Products.Queries
{
    public class GetProductByCategoryIdQuery : IRequest<IEnumerable<GetProductDTO>>
    {
        public int CategoryId { get; set; }
    }

    public class GetProductByCategoryIdQueryHandler : IRequestHandler<GetProductByCategoryIdQuery, IEnumerable<GetProductDTO>>
    {
        private readonly IGeneralRepo<Product> repo;
        private readonly IMediator mediator;

        public GetProductByCategoryIdQueryHandler(IGeneralRepo<Product> repo, IMediator mediator)
        {
            this.repo = repo;
            this.mediator = mediator;
        }
        public async Task<IEnumerable<GetProductDTO>> Handle(GetProductByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var category = await mediator.Send(new GetCategoryByIdQuery() { CategoryId = request.CategoryId });
            if (category == null)
            {
                List<GetProductDTO> x = new List<GetProductDTO>();
                return x;

            }

            return await repo.GetFilter(p => p.IsDeleted == false && p.CategoryId == request.CategoryId).ProjectTo<GetProductDTO>().ToListAsync();
        }
    }
}
