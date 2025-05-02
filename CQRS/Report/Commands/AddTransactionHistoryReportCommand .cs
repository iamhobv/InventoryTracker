using System.Collections.Generic;
using InventoryTracker.CQRS.Categories.Queries;
using InventoryTracker.CQRS.Products.Queries;
using InventoryTracker.Data;
using InventoryTracker.Enums;
using InventoryTracker.Models;
using MediatR;
using RoboostAssessment.DTO.CategoryDTOs;
using RoboostAssessment.DTO.ProductDTOs;
using static InventoryTracker.Enums.ReportType;

namespace InventoryTracker.CQRS.Report.Commands
{
    public class AddTransactionHistoryReportCommand : IRequest<bool>
    {
        public string UserId { get; set; }
    }
    public class AddTransactionHistoryReportCommandHandler : IRequestHandler<AddTransactionHistoryReportCommand, bool>
    {
        private readonly IMediator mediator;
        private readonly IGeneralRepo<Models.Report> repo;

        public AddTransactionHistoryReportCommandHandler(IMediator mediator, IGeneralRepo<Models.Report> repo)
        {
            this.mediator = mediator;
            this.repo = repo;
        }

        public async Task<bool> Handle(AddTransactionHistoryReportCommand request, CancellationToken cancellationToken)
        {
            try
            {

                Models.Report report = new Models.Report()
                {
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                    ReportType = ReportTypeEnum.TransactionHistoryReport,
                    UserId = request.UserId,

                };
                repo.Add(report);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
