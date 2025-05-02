using AutoMapper;
using InventoryTracker.CQRS.Categories.Commands;
using InventoryTracker.Models;
using RoboostAssessment.DTO.CategoryDTOs;

namespace InventoryTracker.DTOs.CategoryDTOs
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, GetCategoriesDTO>()
                .ForMember(dst => dst.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opts => opts.MapFrom(src => src.Description))
                .ForMember(dst => dst.Id, opts => opts.MapFrom(src => src.ID))
                .ReverseMap();

            CreateMap<Category, AddCategoryDTO>()
                .ForMember(dst => dst.CategoryName, opts => opts.MapFrom(src => src.Name))
                .ForMember(dst => dst.CategoryDescription, opts => opts.MapFrom(src => src.Description))
                .ReverseMap();

            //CreateMap<List<Category>, List<GetCategoriesDTO>>().ReverseMap();

            CreateMap<Category, EditCategoryDTO>().ReverseMap();
            CreateMap<UpdateCategoryCommand, EditCategoryDTO>().ReverseMap();
        }
    }
}
