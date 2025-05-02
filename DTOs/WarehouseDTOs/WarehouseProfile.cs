using AutoMapper;
using InventoryTracker.CQRS.Warehouse.Commands;
using InventoryTracker.Models;
using RoboostAssessment.DTO.CategoryDTOs;

namespace InventoryTracker.DTOs.WarehouseDTOs
{
    public class WarehouseProfile : Profile
    {
        public WarehouseProfile()
        {
            CreateMap<Warehouse, AddWarehosueDTO>().ReverseMap();
            CreateMap<Warehouse, GetWarehouseDTO>().ReverseMap();
            CreateMap<Warehouse, UpdateWarehouseDTO>().ReverseMap();
            CreateMap<GetWarehouseDTO, UpdateWarehouseDTO>().ReverseMap();
            CreateMap<AddWarehouseCommand, AddWarehosueDTO>().ReverseMap();
            CreateMap<AddWarehouseCommand, Warehouse>().ReverseMap();
            CreateMap<UpdateWarehouseDTO, UpdateWarehouseCommand>().ReverseMap();


        }
    }
}
