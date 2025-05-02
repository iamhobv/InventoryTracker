using InventoryTracker.CQRS.Categories.Queries;
using InventoryTracker.CQRS.Category.Queries;
using InventoryTracker.Data;
using InventoryTracker.Services;
using MediatR;
using RoboostAssessment.DTO.CategoryDTOs;

namespace InventoryTracker.CQRS.Categories.Commands
{
    public class UpdateCategoryCommand : IRequest<bool>
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IGeneralRepo<Models.Category> categoryRepo;
        private readonly IMediator mediator;

        public UpdateCategoryCommandHandler(IGeneralRepo<Models.Category> categoryRepo, IMediator mediator)
        {
            this.categoryRepo = categoryRepo;
            this.mediator = mediator;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await mediator.Send(new GetCategoryForEditQuery() { CategoryId = request.CategoryId });
            if (existingCategory == null)
            {
                return false;
            }

            existingCategory.Description = request.Description ?? existingCategory.Description;
            existingCategory.Name = request.Name ?? existingCategory.Name;
            categoryRepo.Update(existingCategory);
            return true;
        }
    }
}
