using AutoMapper;
using Tech_Manage_Server.DTOs.CustomerModelDto;
using Tech_Manage_Server.DTOs.InventoryModelDto;
using Tech_Manage_Server.DTOs.RepairModelDto;
using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Repair, CreateRepairDto>().ReverseMap();
            CreateMap<Repair,UpdateRepairDto>().ReverseMap();
            CreateMap<Inventory, InventoryDto>();
            CreateMap<CreateInventoryDto, Inventory>();
            CreateMap<UpdateInventoryDto, Inventory>();
            CreateMap<Customer, CustomerDto>();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<UpdateCustomerDto, Customer>();
        }
    }
}
