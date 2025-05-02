using AutoMapper;
using InventoryTracker.CQRS.Notification.Commands;
using InventoryTracker.Models;

namespace InventoryTracker.DTOs.NorificationDTOs
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<AddNotificationCommand, Notification>().ReverseMap();
        }
    }
}
