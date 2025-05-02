using InventoryTracker.CQRS.Category.Queries;
using InventoryTracker.Data;
using InventoryTracker.Services;
using MediatR;
using RoboostAssessment.DTO.CategoryDTOs;

namespace InventoryTracker.CQRS.Categories.Queries
{

    public class GetCategoryByIdQuery : IRequest<GetCategoriesDTO>
    {
        public int CategoryId { get; set; }

    }

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, GetCategoriesDTO>
    {
        private readonly IGeneralRepo<Models.Category> categoryRepo;

        public GetCategoryByIdQueryHandler(IGeneralRepo<Models.Category> categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }
        public async Task<GetCategoriesDTO> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return categoryRepo.GetByID(request.CategoryId).Map<GetCategoriesDTO>();
        }

    }
}
