using AutoMapper;
using Tech_Manage_Server.Application.DTOs.ImageDto;
using Tech_Manage_Server.Domain.Models;
using Tech_Manage_Server.DTOs.CustomerModelDto;
using Tech_Manage_Server.DTOs.InventoryModelDto;
using Tech_Manage_Server.DTOs.RepairItemModelDto;
using Tech_Manage_Server.DTOs.RepairModelDto;
using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Map Repair
            CreateMap<Repair, RepairDto>().ReverseMap();
            CreateMap<CreateRepairDto, Repair>().ReverseMap();
            CreateMap<UpdateRepairDto, Repair>().ReverseMap();

            // Map RepairItem
            CreateMap<RepairItemDto, RepairItem>().ReverseMap();
            CreateMap<CreateRepairItemDto, RepairItem>().ReverseMap();
            CreateMap<UpdateRepairItemDto, RepairItem>().ReverseMap();

            // Map Inventory
            CreateMap<Inventory, InventoryDto>().ReverseMap();
            CreateMap<CreateInventoryDto, Inventory>().ReverseMap();
            CreateMap<UpdateInventoryDto, Inventory>().ReverseMap();

            // Map Customer
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CreateCustomerDto, Customer>().ReverseMap();
            CreateMap<UpdateCustomerDto, Customer>().ReverseMap();

            // Map Image
            CreateMap<ImageSource, ImageSourceDto>().ReverseMap();
        }
    }
}
