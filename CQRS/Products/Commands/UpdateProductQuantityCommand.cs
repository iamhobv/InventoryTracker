using InventoryTracker.CQRS.Categories.Queries;
using InventoryTracker.CQRS.Products.Queries;
using InventoryTracker.Data;
using InventoryTracker.Models;
using MediatR;
using RoboostAssessment.DTO.CategoryDTOs;

namespace InventoryTracker.CQRS.Products.Commands
{
    public class UpdateProductQuantityCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

    }
    public class UpdateProductQuantityCommandHandler : IRequestHandler<UpdateProductQuantityCommand, bool>
    {
        private readonly IMediator mediator;
        private readonly IGeneralRepo<Product> repo;

        public UpdateProductQuantityCommandHandler(IMediator mediator, IGeneralRepo<Models.Product> repo)
        {
            this.mediator = mediator;
            this.repo = repo;
        }
        public async Task<bool> Handle(UpdateProductQuantityCommand request, CancellationToken cancellationToken)
        {
            var isProductExist = await mediator.Send(new GetProductByIDForQuantityUpdateQuery() { Id = request.Id });
            if (isProductExist == null)
            {
                return false;
            }
            //isProductExist.Quantity = request.Quantity + isProductExist.Quantity;



            Product OldProduct = await mediator.Send(new GetProductByIDForQuantityUpdateQuery() { Id = request.Id });


            if (OldProduct == null)
            {
                return false;
            }

            if (request.Quantity < 0 && -request.Quantity > OldProduct.Quantity)
            {
                OldProduct.Quantity = 0;
            }
            else
            {
                OldProduct.Quantity = request.Quantity + OldProduct.Quantity;
            }

            //var newQuantity = OldProduct.Quantity + request.Quantity;
            //OldProduct.Quantity = newQuantity;
            repo.Update(OldProduct);
            return true;

        }
    }
}
