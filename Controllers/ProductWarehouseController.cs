using System.Threading.Tasks;
using InventoryTracker.CQRS;
using InventoryTracker.CQRS.ProductWarehouse.Commands;
using InventoryTracker.Data;
using InventoryTracker.DTOs.ProductWarehouseDTOs;
using InventoryTracker.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductWarehouseController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductWarehouseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<GeneralResponse>> AddProductWarehouse(AddProductWarehouseDTO productWarehouseDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    AddProductWarehouseCommand productWarehouseCommand = productWarehouseDTO.Map<AddProductWarehouseCommand>();

                    var result = await mediator.Send(productWarehouseCommand);
                    if (result)
                    {
                        await mediator.Send(new SaveChanges());
                        return new GeneralResponse()
                        {
                            IsPass = true,
                            Data = "ProductWarehouse Object has been added!"
                        };
                    }
                    return new GeneralResponse()
                    {
                        IsPass = false,
                        Data = "Check product or warehosue!"
                    };

                }
                return new GeneralResponse()
                {
                    IsPass = false,
                    Data = ModelState
                };
            }
            catch (Exception)
            {

                return new GeneralResponse()
                {
                    IsPass = false,
                    Data = "Unexpected error!"
                };
            }
           
        }
    }
}
