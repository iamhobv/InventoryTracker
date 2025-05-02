using InventoryTracker.Data;
using MediatR;
using RoboostAssessment.DTO.CategoryDTOs;

namespace InventoryTracker.CQRS.Categories.Queries
{
    public class GetCategoryForEditQuery : IRequest<Models.Category>
    {
        public int CategoryId { get; set; }

    }

    public class GetCategoryForEditQueryHandler : IRequestHandler<GetCategoryForEditQuery, Models.Category>
    {
        private readonly IGeneralRepo<Models.Category> categoryRepo;

        public GetCategoryForEditQueryHandler(IGeneralRepo<Models.Category> categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }
        public async Task<Models.Category> Handle(GetCategoryForEditQuery request, CancellationToken cancellationToken)
        {
            return categoryRepo.GetByID(request.CategoryId);
            //return Task.CompletedTask;
        }

    }
}
