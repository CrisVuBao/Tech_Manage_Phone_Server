using AutoMapper;
using Tech_Manage_Server.DTOs;
using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Repair, CreateRepairDto>().ReverseMap();
            CreateMap<Repair,UpdateRepairDto>().ReverseMap();
        }
    }
}
