using InventoryTracker.CQRS.Category.Queries;
using InventoryTracker.Data;
using InventoryTracker.Services;
using MediatR;
using RoboostAssessment.DTO.CategoryDTOs;

namespace InventoryTracker.CQRS.Categories.Queries
{
    public class GetAllCategoryQuery : IRequest<IEnumerable<GetCategoriesDTO>>
    {
    }
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, IEnumerable<GetCategoriesDTO>>
    {
        private readonly IGeneralRepo<Models.Category> categoryRepo;

        public GetAllCategoryQueryHandler(IGeneralRepo<Models.Category> categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        public async Task<IEnumerable<GetCategoriesDTO>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            return categoryRepo.GetAll().ProjectTo<GetCategoriesDTO>().ToList();
        }
    }
}
