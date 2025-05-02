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
    public class AddProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int LowStockThreshold { get; set; }
        public int CategoryId { get; set; }
    }
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, int>
    {
        private readonly IGeneralRepo<Product> repo;
        private readonly IMediator mediator;

        public AddProductCommandHandler(IGeneralRepo<Product> repo, IMediator mediator)
        {
            this.repo = repo;
            this.mediator = mediator;
        }
        public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Product product = request.Map<Product>();

                repo.Add(product);
                repo.Save();
                return product.ID; ;
            }
            catch (Exception)
            {

                return -1;
            }


        }
    }
}
