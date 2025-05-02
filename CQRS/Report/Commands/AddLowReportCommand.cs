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
    public class AddLowReportCommand : IRequest<IEnumerable<GetProductDTO>>
    {
        public string UserId { get; set; }
        public int? CategoryId { get; set; }
    }
    public class AddLowReportCommandHandler : IRequestHandler<AddLowReportCommand, IEnumerable<GetProductDTO>>
    {
        private readonly IMediator mediator;
        private readonly IGeneralRepo<Models.Report> repo;

        public AddLowReportCommandHandler(IMediator mediator, IGeneralRepo<Models.Report> repo)
        {
            this.mediator = mediator;
            this.repo = repo;
        }

        public async Task<IEnumerable<GetProductDTO>> Handle(AddLowReportCommand request, CancellationToken cancellationToken)
        {
            List<GetProductDTO> List = new List<GetProductDTO>();
            if (request.CategoryId != null)
            {
                var category = await mediator.Send(new GetCategoryByIdQuery() { CategoryId = (int)request.CategoryId });

                if (category != null)
                {

                    List = (List<GetProductDTO>)await mediator.Send(new GetProductLessThanThresholdByCategoryQuery() { CategoryId = (int)request.CategoryId });
                }

            }

            List = (List<GetProductDTO>)await mediator.Send(new GetProductLessThanThresholdQuery());
            Models.Report report = new Models.Report()
            {
                CreationDate = DateTime.Now,
                IsDeleted = false,
                ReportType = ReportTypeEnum.LowStockReport,
                UserId = request.UserId,

            };
            repo.Add(report);
            return List;
        }
    }
}
