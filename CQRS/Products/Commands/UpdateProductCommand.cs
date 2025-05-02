using InventoryTracker.CQRS.Categories.Queries;
using InventoryTracker.CQRS.Products.Queries;
using InventoryTracker.Data;
using InventoryTracker.Models;
using InventoryTracker.Services;
using MediatR;
using RoboostAssessment.DTO.CategoryDTOs;
using RoboostAssessment.DTO.ProductDTOs;

namespace InventoryTracker.CQRS.Products.Commands
{
    public class UpdateProductCommand : IRequest<bool>

    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public int? LowStockThreshold { get; set; }

        public int? CategoryId { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IMediator mediator;
        private readonly IGeneralRepo<Product> repo;

        public UpdateProductCommandHandler(IMediator mediator, IGeneralRepo<Models.Product> repo)
        {
            this.mediator = mediator;
            this.repo = repo;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product OldProduct = await mediator.Send(new GetProductForUpdateByIdQuery() { Id = request.Id });
            GetCategoriesDTO existingCategory = null;
            if (existingCategory == null && request.CategoryId.HasValue)
            {

                existingCategory = await mediator.Send(new GetCategoryByIdQuery() { CategoryId = (int)request.CategoryId });
            }

            if (OldProduct == null)
            {
                return false;
            }
            //Product product = OldProduct.Map<Product>();

            OldProduct.Name = request.Name ?? OldProduct.Name;
            OldProduct.Description = request.Description ?? OldProduct.Description;
            OldProduct.Price = (double)(request.Price ?? OldProduct.Price);
            OldProduct.LowStockThreshold = (int)(request.LowStockThreshold ?? OldProduct.LowStockThreshold);
            OldProduct.CategoryId = (int)(request.CategoryId ?? OldProduct.CategoryId);
            //Product.IsDeleted= OldProduct.isd);
            repo.Update(OldProduct);
            return true;



        }
    }
}
