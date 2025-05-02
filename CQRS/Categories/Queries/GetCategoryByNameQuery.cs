using AutoMapper.QueryableExtensions;
using InventoryTracker.Data;
using InventoryTracker.Services;
using MediatR;
using RoboostAssessment.DTO.CategoryDTOs;

namespace InventoryTracker.CQRS.Category.Queries
{
    public class GetCategoryByNameQuery : IRequest<GetCategoriesDTO>
    {
        public string CategoryName { get; set; }
    }


    public class GetCategoryByNameQueryHandler : IRequestHandler<GetCategoryByNameQuery, GetCategoriesDTO>
    {
        private readonly IGeneralRepo<Models.Category> categoryRepo;

        public GetCategoryByNameQueryHandler(IGeneralRepo<Models.Category> categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }
        public async Task<GetCategoriesDTO> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            return categoryRepo.GetFilter(x => x.Name.Equals(request.CategoryName)).ProjectTo<GetCategoriesDTO>().FirstOrDefault();
            //return Task.CompletedTask;
        }

    }
}
