using InventoryTracker.CQRS.Categories.Queries;
using InventoryTracker.Data;
using MediatR;

namespace InventoryTracker.CQRS.Categories.Commands
{
    public class DeleteCategoryCommand : IRequest<bool>
    {
        public int CategoryID { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IGeneralRepo<Models.Category> categoryRepo;
        private readonly IMediator mediator;

        public DeleteCategoryCommandHandler(IGeneralRepo<Models.Category> categoryRepo, IMediator mediator)
        {
            this.categoryRepo = categoryRepo;
            this.mediator = mediator;
        }
        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await mediator.Send(new GetCategoryForEditQuery() { CategoryId = request.CategoryID });
            if (existingCategory == null)
            {
                return false;
            }
            categoryRepo.Remove(request.CategoryID);
            return true;
        }
    }
}
