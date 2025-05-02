using AutoMapper;
using InventoryTracker.CQRS.InventoryTransactions.Commands;
using InventoryTracker.CQRS.InventoryTransactions.Orchestrators;
using InventoryTracker.CQRS.ProductWarehouse.Commands;
using InventoryTracker.Models;
using RoboostAssessment.DTO.TransactionDTOs;

namespace InventoryTracker.DTOs.TransactionDTOs
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<AddInventoryOrchesterator, AddInventoryTranactionCommand>().ReverseMap();
            CreateMap<AddInventoryOrchesterator, AddProductWarehouseCommand>().ReverseMap();
            CreateMap<AddInventoryOrchesterator, UpdateProductWarehouseCommand>().ReverseMap();
            CreateMap<RemoveInventoryOrchesterator, AddInventoryTranactionCommand>().ReverseMap();
            CreateMap<AddInventoryTranactionCommand, GetTTansactionToReportsDTO>().ReverseMap();
            CreateMap<InventoryTransaction, GetTTansactionToReportsDTO>().ReverseMap();
            CreateMap<InventoryTransaction, GetTTansactionToReportsDTO>().ReverseMap();

            CreateMap<InventoryTransaction, ArchivedInventoryTransaction>().ForMember(dest=>dest.ID,opt=>opt.Ignore()).ReverseMap();

            //RemoveInventoryOrchesterator->AddInventoryTranactionCommand
            //InventoryTransaction->GetTTansactionToReportsDTO

        }
    }
}
