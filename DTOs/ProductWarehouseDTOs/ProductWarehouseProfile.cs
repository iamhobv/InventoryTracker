using AutoMapper;
using InventoryTracker.CQRS.ProductWarehouse.Commands;
using InventoryTracker.Models;

namespace InventoryTracker.DTOs.ProductWarehouseDTOs
{
    public class ProductWarehouseProfile : Profile
    {
        public ProductWarehouseProfile()
        {

            CreateMap<ProductWarehouse, GetProductWarehouseDTO>()
                .ForMember(dst => dst.ProductName, opts => opts.MapFrom(src => src.Product.Name))
                .ForMember(dst => dst.Price, opts => opts.MapFrom(src => src.Product.Price))
                .ForMember(dst => dst.WarehouseName, opts => opts.MapFrom(src => src.Warehouse.Name))
                .ForMember(dst => dst.WarehouseLocation, opts => opts.MapFrom(src => src.Warehouse.Location));

            CreateMap<GetProductWarehouseDTO, ProductWarehouse>();
            CreateMap<AddProductWarehouseCommand, ProductWarehouse>().ReverseMap();
            CreateMap<AddProductWarehouseCommand, AddProductWarehouseDTO>().ReverseMap();
            CreateMap<ProductWarehouse, CheckProductWarehouseByIdDTO>().ReverseMap();
        }
    }
}
