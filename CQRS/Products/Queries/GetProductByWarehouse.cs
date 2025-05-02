using InventoryTracker.CQRS.ProductWarehouse.Queries;
using InventoryTracker.Data;
using InventoryTracker.DTOs.ProductWarehouseDTOs;
using InventoryTracker.Models;
using InventoryTracker.Services;
using MediatR;
using RoboostAssessment.DTO.ProductDTOs;

namespace InventoryTracker.CQRS.Products.Queries
{
    public class GetProductsByWarehouse : IRequest<IEnumerable<GetProductDTO>>
    {
        public int WarehouseID { get; set; }
    }

    public class GetProductsByWarehouseHandler : IRequestHandler<GetProductsByWarehouse, IEnumerable<GetProductDTO>>
    {
        private readonly IGeneralRepo<Product> repo;
        private readonly IMediator mediator;

        public GetProductsByWarehouseHandler(IGeneralRepo<Product> repo, IMediator mediator)
        {
            this.repo = repo;
            this.mediator = mediator;
        }
        public async Task<IEnumerable<GetProductDTO>> Handle(GetProductsByWarehouse request, CancellationToken cancellationToken)
        {
            var products = await mediator.Send(new GetProductWarehouseByPRoductIdQuery() { WarehouseId = request.WarehouseID });
            //List<GetProductWarehouseDTO> gets = products.ToList();
            //foreach (var product in products)
            //{
            //    product.
            //}
            var ProductDTO = products.ProjectEnumrableTo<GetProductWarehouseProductsDTO, GetProductDTO>();
            return ProductDTO;
        }
    }

}
