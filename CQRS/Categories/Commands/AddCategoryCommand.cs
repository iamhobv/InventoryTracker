using InventoryTracker.CQRS.Category.Queries;
using InventoryTracker.Data;
using InventoryTracker.Models;
using MediatR;
using RoboostAssessment.DTO.CategoryDTOs;

namespace InventoryTracker.CQRS.Category.Commands
{
    public class AddCategoryCommand : IRequest<bool>
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
    }
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, bool>
    {
        private readonly IGeneralRepo<Models.Category> categoryRepo;
        private readonly IMediator mediator;

        public AddCategoryCommandHandler(IGeneralRepo<Models.Category> categoryRepo, IMediator mediator)
        {
            this.categoryRepo = categoryRepo;
            this.mediator = mediator;
        }
        public async Task<bool> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.CategoryName))
            {
                return false; // Invalid input
            }

            var existingCategory = await mediator.Send(new GetCategoryByNameQuery() { CategoryName = request.CategoryName });

            if (existingCategory != null)
            {
                return false;
            }
            categoryRepo.Add(new Models.Category { Name = request.CategoryName, Description = request.CategoryDescription });
            return true;

        }
    }
}
