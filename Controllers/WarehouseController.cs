using System.Threading.Tasks;
using InventoryTracker.CQRS;
using InventoryTracker.CQRS.Warehouse.Commands;
using InventoryTracker.CQRS.Warehouse.Queries;
using InventoryTracker.Data;
using InventoryTracker.DTOs.WarehouseDTOs;
using InventoryTracker.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IMediator mediator;

        public WarehouseController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost("Add")]
        public async Task<ActionResult<GeneralResponse>> AddWarehouse(AddWarehosueDTO addWarehosueDTO)
        {
            if (ModelState.IsValid)
            {
                AddWarehouseCommand addWarehouse = addWarehosueDTO.Map<AddWarehouseCommand>();


                var res = await mediator.Send(addWarehouse);
                if (res)
                {
                    await mediator.Send(new SaveChanges());
                    return new GeneralResponse()
                    {
                        IsPass = true,
                        Data = "Warehouse created successfully"
                    };

                }
                return new GeneralResponse()
                {
                    IsPass = false,
                    Data = "Warehouse already exists"
                };
            }
            return new GeneralResponse()
            {
                IsPass = false,
                Data = ModelState
            };

        }


        [HttpGet("Get")]

        public async Task<ActionResult<GeneralResponse>> GetAllWarehouses()
        {
            var warehouses = await mediator.Send(new GetAllWarehosueQuery());

            if (warehouses.Count() == 0)
            {
                return new GeneralResponse()
                {
                    IsPass = false,
                    Data = "No warehouses!"
                };
            }
            return new GeneralResponse()
            {
                IsPass = true,
                Data = warehouses
            };

        }




        [HttpPut("Update")]
        public async Task<ActionResult<GeneralResponse>> UpdateWarehouseAsync(UpdateWarehouseDTO warehouseUpdate)
        {
            if (ModelState.IsValid)
            {
                UpdateWarehouseCommand updateWarehouseCommand = warehouseUpdate.Map<UpdateWarehouseCommand>();
                var result = await mediator.Send(updateWarehouseCommand);

                if (result)
                {

                    await mediator.Send(new SaveChanges());
                    return new GeneralResponse()
                    {
                        IsPass = true,
                        Data = "Warehouse has been updated"
                    };
                }

                return new GeneralResponse()
                {
                    IsPass = false,
                    Data = "Warehouse is not exist"
                };
            }
            return new GeneralResponse()
            {
                IsPass = false,
                Data = ModelState
            };
        }


        [HttpDelete("Delete/{id:int}")]
        public async Task<ActionResult<GeneralResponse>> DeleteWarehouseAsync(int id)
        {
            var res = await mediator.Send(new DeleteWarehouseCommand() { Id = id });
            if (res)
            {
                await mediator.Send(new SaveChanges());
                return new GeneralResponse()
                {
                    IsPass = true,
                    Data = "Warehouse has been deleted"
                };
            }
            return new GeneralResponse()
            {
                IsPass = false,
                Data = "Warehouse is not exist"
            };

        }
    }
}
