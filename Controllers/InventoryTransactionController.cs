using InventoryTracker.CQRS;
using InventoryTracker.CQRS.InventoryTransactions.Commands;
using InventoryTracker.CQRS.InventoryTransactions.Orchestrators;
using InventoryTracker.Data;
using InventoryTracker.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoboostAssessment.DTO.TransactionDTOs;
using static InventoryTracker.Enums.TransactionTypes;

namespace InventoryTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryTransactionController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly UserManager<ApplicationUser> userManager;

        public InventoryTransactionController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        [HttpPost("AddStock")]
        public async Task<ActionResult<GeneralResponse>> AddToStock(AddStockDTO addStock)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser? currentUser = await userManager.GetUserAsync(User);
                    if (currentUser == null || currentUser.UserName != addStock.UserName)
                    {
                        return new GeneralResponse()
                        {
                            IsPass = false,
                            Data = "Unauthorized access or user mismatch"
                        };
                    }


                    var res = await mediator.Send(new AddInventoryOrchesterator() { ProductId = addStock.ProductId, Quantity = addStock.Quantity, UserId = currentUser.Id, WarehouseId = addStock.WarehouseId, TransactionsType = TransactionsTypeEnum.Add });

                    if (res)
                    {


                        await mediator.Send(new SaveChanges());
                        return new GeneralResponse()
                        {
                            IsPass = true,
                            Data = "Product has been updated"
                        };
                    }
                    return new GeneralResponse()
                    {
                        IsPass = false,
                        Data = "Product is not exist"
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
                    Data = "Unexpicted Error"
                };
            }

        }


        [HttpPost("RemoveStock")]
        public async Task<ActionResult<GeneralResponse>> RemoveFromStock(RemoveStockDTO RemoveStock)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser? currentUser = await userManager.GetUserAsync(User);
                if (currentUser == null || currentUser.UserName != RemoveStock.UserName)
                {
                    return new GeneralResponse()
                    {
                        IsPass = false,
                        Data = "Unauthorized access or user mismatch"
                    };
                }


                var res = await mediator.Send(new RemoveInventoryOrchesterator() { ProductId = RemoveStock.ProductId, Quantity = RemoveStock.Quantity, UserId = currentUser.Id, WarehouseId = RemoveStock.WarehouseId, TransactionsType = TransactionsTypeEnum.Remove });

                if (res)
                {


                    await mediator.Send(new SaveChanges());
                    return new GeneralResponse()
                    {
                        IsPass = true,
                        Data = "Product has been updated"
                    };
                }
                return new GeneralResponse()
                {
                    IsPass = false,
                    Data = "Product is not exist"
                };


            }
            return new GeneralResponse()
            {
                IsPass = false,
                Data = ModelState
            };
            //try
            //{
            //}
            //catch (Exception)
            //{

            //    return new GeneralResponse()
            //    {
            //        IsPass = false,
            //        Data = "Unexpicted Error"
            //    };
            //}

        }



        [HttpPost("TransferStock")]
        public async Task<ActionResult<GeneralResponse>> TransferStock(TransfereStockDTO transfereStock)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser? currentUser = await userManager.GetUserAsync(User);
                if (currentUser == null || currentUser.UserName != transfereStock.UserName)
                {
                    return new GeneralResponse()
                    {
                        IsPass = false,
                        Data = "Unauthorized access or user mismatch"
                    };
                }
                var res = await mediator.Send(new TransfereInventoryCommand()
                {
                    ProductID = transfereStock.ProductId,
                    UserID = currentUser.Id,
                    NewWarehouseID = transfereStock.NewWarehouseID,
                    OldWarehouseID = transfereStock.OldWarehouseID

                });

                if (res)
                {
                    await mediator.Send(new SaveChanges());
                    return new GeneralResponse()
                    {
                        IsPass = true,
                        Data = "Product has been updated"
                    };
                }
                return new GeneralResponse()
                {
                    IsPass = false,
                    Data = "Check Inventory or Product"
                };

            }
            return new GeneralResponse()
            {
                IsPass = false,
                Data = ModelState
            };


        }

    }
}
