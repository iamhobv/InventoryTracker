using AutoMapper;
using InventoryTracker.CQRS.Categories.Queries;
using InventoryTracker.CQRS.Products.Queries;
using InventoryTracker.Data;
using InventoryTracker.Models;
using InventoryTracker.Services;
using MediatR;
using RoboostAssessment.DTO.ProductDTOs;

namespace InventoryTracker.CQRS.Products.Commands
{
    public class AddProductCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int LowStockThreshold { get; set; }
        public int CategoryId { get; set; }
    }
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, bool>
    {
        private readonly IGeneralRepo<Product> repo;
        private readonly IMediator mediator;

        public AddProductCommandHandler(IGeneralRepo<Product> repo, IMediator mediator)
        {
            this.repo = repo;
            this.mediator = mediator;
        }
        public async Task<bool> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            GetProductDTO existingProduct = await mediator.Send(new GetProductByNameNotDeletedQuery() { Name = request.Name });
            var existingCategory = await mediator.Send(new GetCategoryByIdQuery() { CategoryId = request.CategoryId });

            if (existingProduct != null || existingCategory == null)
            {
                return false;
            }

            Product product = request.Map<Product>();
            repo.Add(product);
            return true;

        }
    }
}
