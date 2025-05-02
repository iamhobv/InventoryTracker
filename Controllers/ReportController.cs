using InventoryTracker.CQRS;
using InventoryTracker.CQRS.Report.Commands;
using InventoryTracker.Data;
using InventoryTracker.DTOs.ReportDTOs;
using InventoryTracker.Enums;
using InventoryTracker.Models;
using InventoryTracker.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RoboostAssessment.DTO.ProductDTOs;
using RoboostAssessment.DTO.TransactionDTOs;

namespace InventoryTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITransactionHistoryReportBuilder transactionHistoryReportBuilder;

        public ReportController(IMediator mediator, UserManager<ApplicationUser> userManager, ITransactionHistoryReportBuilder transactionHistoryReportBuilder)
        {
            this.mediator = mediator;
            this.userManager = userManager;
            this.transactionHistoryReportBuilder = transactionHistoryReportBuilder;
        }

        [HttpPost("LowStock")]
        public async Task<ActionResult<GeneralResponse>> LowStockReports(AddLowReportDTO addLowStockReport)
        {
            ApplicationUser? currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null || currentUser.UserName != addLowStockReport.UserName)
            {
                return new GeneralResponse()
                {
                    IsPass = false,
                    Data = "Unauthorized access or user mismatch"
                };
            }

            if (ModelState.IsValid)
            {

                var product = await mediator.Send(new AddLowReportCommand() { CategoryId = addLowStockReport.CategoryId, UserId = currentUser.Id });
                await mediator.Send(new SaveChanges());
                if (product.Count() == 0)
                {
                    return new GeneralResponse()
                    {
                        IsPass = false,
                        Data = "No product with low stock "
                    };
                }
                return new GeneralResponse()
                {
                    IsPass = true,
                    Data = product
                };



            }
            return new GeneralResponse()
            {
                IsPass = false,
                Data = ModelState
            };




        }


        [HttpPost("TransactionHistory")]
        public async Task<ActionResult<GeneralResponse>> TransactionHistoryReports(AddTransactionHistoryDTO addTransactionHistory)
        {
            ApplicationUser? currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null || currentUser.UserName != addTransactionHistory.UserName)
            {
                return new GeneralResponse()
                {
                    IsPass = false,
                    Data = "Unauthorized access or user mismatch"
                };
            }


            ITransactionHistoryReportBuilder builder = transactionHistoryReportBuilder
                .SetCategory(addTransactionHistory.CategoryId)
                .SetProduct(addTransactionHistory.ProductId)
                .SetDateRange(addTransactionHistory.StratingDate, addTransactionHistory.EndingDate)
                .SetTransactionsType(addTransactionHistory.TransactionsType);

            IEnumerable<GetTTansactionToReportsDTO> result = await builder.Build();


            //Report report = new Report()
            //{
            //    CreationDate = DateTime.Now,
            //    IsDeleted = false,
            //    ReportType = ReportType.ReportTypeEnum.TransactionHistoryReport,
            //    UserId = currentUser.Id,

            //};
            //reportService.Add(report);
            //reportService.Save();

            return new GeneralResponse()
            {
                IsPass = true,
                Data = result
            };
        }

    }
}
