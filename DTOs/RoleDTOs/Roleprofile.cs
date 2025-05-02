using AutoMapper;
using InventoryTracker.Models;

namespace InventoryTracker.DTOs.RoleDTOs
{
    public class Roleprofile : Profile
    {
        public Roleprofile()
        {
            CreateMap<ApplicationUser, UsernameDTO>()
           .ForMember(dst => dst.Username, opts => opts.MapFrom(src => src.UserName))
           .ReverseMap();

            CreateMap<ApplicationRole, RoleNameDTO>()
           .ForMember(dst => dst.RoleName, opts => opts.MapFrom(src => src.Name))
           .ReverseMap();

        }
    }
}
