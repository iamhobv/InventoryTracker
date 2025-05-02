using InventoryTracker.CQRS.Categories.Queries;
using InventoryTracker.CQRS.InventoryTransactions.Commands;
using InventoryTracker.CQRS.Products.Commands;
using InventoryTracker.CQRS.Products.Queries;
using InventoryTracker.CQRS.ProductWarehouse.Commands;
using InventoryTracker.CQRS.Warehouse.Queries;
using InventoryTracker.Data;
using InventoryTracker.Models;
using InventoryTracker.Services;
using MediatR;
using RoboostAssessment.DTO.ProductDTOs;

namespace InventoryTracker.CQRS.Products.Orchestrators
{
    public class AddProductOrchestrator : IRequest<bool>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int LowStockThreshold { get; set; }
        public int CategoryId { get; set; }
        public int WarehosueId { get; set; }
        public string UserId { get; set; }
    }

    public class AddProductOrchestratorHandler : IRequestHandler<AddProductOrchestrator, bool>
    {
        private readonly IMediator mediator;
        private readonly IGeneralRepo<Product> repo;

        public AddProductOrchestratorHandler(IMediator mediator, IGeneralRepo<Product> repo)
        {
            this.mediator = mediator;
            this.repo = repo;
        }
        public async Task<bool> Handle(AddProductOrchestrator request, CancellationToken cancellationToken)
        {
            try
            {
                GetProductDTO existingProduct = await mediator.Send(new GetProductByNameNotDeletedQuery() { Name = request.Name });
                var existingWarehouse = await mediator.Send(new GetWarehouseByIdNotDeletedQuery() { Id = request.WarehosueId });
                var existingCategory = await mediator.Send(new GetCategoryByIdQuery() { CategoryId = request.CategoryId });

                if (existingProduct != null || existingCategory == null || existingWarehouse == null)
                {
                    return false;
                }
                AddProductCommand productCommand = request.Map<AddProductCommand>();
                var addProductResult = await mediator.Send(productCommand);
                if (addProductResult == -1)
                {
                    return false;
                }

                bool addProductWarehouseResult = await mediator.Send(new AddProductWarehouseCommand()

                {
                    ProductID = addProductResult,
                    ProductQuantity = request.Quantity,
                    WarehouseId = request.WarehosueId
                });

                if (addProductWarehouseResult)
                {
                    var result = await mediator.Send(new AddInventoryTranactionCommand() { ProductId = addProductResult, Quantity = request.Quantity, TransactionsType = Enums.TransactionTypes.TransactionsTypeEnum.Add, WarehouseId = request.WarehosueId, UserId = request.UserId });
                    return true;

                }
                return false;



            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
