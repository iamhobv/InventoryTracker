using InventoryTracker.CQRS.Products.Queries;
using InventoryTracker.Data;
using InventoryTracker.Models;
using MediatR;

namespace InventoryTracker.CQRS.Products.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IGeneralRepo<Product> repo;
        private readonly IMediator mediator;

        public DeleteProductCommandHandler(IGeneralRepo<Product> repo, IMediator mediator)
        {
            this.repo = repo;
            this.mediator = mediator;
        }
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product existingProduct = await mediator.Send(new GetProductForUpdateByIdQuery() { Id = request.Id });
            if (existingProduct == null)
            {
                return false;
            }
            existingProduct.IsDeleted = true;
            repo.Update(existingProduct);
            return true;


        }
    }
}
