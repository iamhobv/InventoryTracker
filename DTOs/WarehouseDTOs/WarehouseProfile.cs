using AutoMapper;
using InventoryTracker.CQRS.ProductWarehouse.Commands;
using InventoryTracker.CQRS.Warehouse.Commands;
using InventoryTracker.DTOs.ProductWarehouseDTOs;
using InventoryTracker.Models;
using RoboostAssessment.DTO.CategoryDTOs;
using RoboostAssessment.DTO.ProductDTOs;

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
            CreateMap<ProductWarehouse, AddProductWarehouseCommand>().ReverseMap();


            CreateMap<GetProductWarehouseDTO, GetProductDTO>()
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.ProductQuantity))//.ReverseMap();
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))//.ReverseMap();
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductID))//.ReverseMap();
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductID)).ReverseMap();


            CreateMap<GetProductWarehouseProductsDTO, GetProductDTO>()
              .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.ProductQuantity))//.ReverseMap();
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))//.ReverseMap();
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductID))//.ReverseMap();
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductID))//.ReverseMap();
              .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))//.ReverseMap();
              .ForMember(dest => dest.LowStockThreshold, opt => opt.MapFrom(src => src.LowStockThreshold))//.ReverseMap();
              .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId)).ReverseMap();
            //.ForMember(dest => dest.cate, opt => opt.MapFrom(src => src.CategoryId))//.ReverseMap();


            CreateMap<ProductWarehouse, GetProductWarehouseProductsDTO>()//.ReverseMap();
                .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.ProductQuantity, opt => opt.MapFrom(src => src.Product.Quantity))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Product.Description))
                .ForMember(dest => dest.LowStockThreshold, opt => opt.MapFrom(src => src.Product.LowStockThreshold))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Product.CategoryId)).ReverseMap();

            /*
             1
                [ForeignKey("Warehouse")]
        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }


        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public Product? Product { get; set; }


        public int ProductQuantity { get; set; }



            /*
             2
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }


        public int ProductQuantity { get; set; }
        public string Description { get; set; }
        public int LowStockThreshold { get; set; }

        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
            */

        }
    }
}
