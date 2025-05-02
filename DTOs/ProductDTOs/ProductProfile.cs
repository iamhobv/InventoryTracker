using AutoMapper;
using InventoryTracker.CQRS.Products.Commands;
using InventoryTracker.Models;
using RoboostAssessment.DTO.CategoryDTOs;
using RoboostAssessment.DTO.ProductDTOs;

namespace InventoryTracker.DTOs.ProductDTOs
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductDTO>().ReverseMap();
            //.ForMember(dst => dst.WarehouseName, opts => opts.MapFrom(src => src.ProductWarehouse));
            //.ForMember(dst => dst.WarehouseLocation, opts => opts.MapFrom(src => src.ProductWarehouse));


            CreateMap<Product, AddProductDTO>().ReverseMap();
            CreateMap<AddProductCommand, AddProductDTO>().ReverseMap();
            CreateMap<AddProductCommand, Product>().ReverseMap();
            CreateMap<UpdateProductDTO, UpdateProductCommand>().ReverseMap();
            CreateMap<UpdateProductDTO, GetProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
            CreateMap<Product, Product>().ReverseMap();
        }
    }
}
/*
  public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int LowStockThreshold { get; set; }

        public string CategoryName { get; set; }

        public string InventoryName { get; set; }
        public string InventoryLocation { get; set; }
 */