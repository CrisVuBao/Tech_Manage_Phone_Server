using Tech_Manage_Server.DTOs;
using Tech_Manage_Server.Helpers;
using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.Repositories.Interface
{
    public interface IRepairRepository
    {
        Task<List<Repair>> GetAllRepairAsync();
        Task<Repair> GetRepairWithIdAsync(int id);
        Task<Response<Repair>> CreateRepairAsync(CreateRepairDto createRepairDto);
        Task<Repair> UpdateRepairAsync(Repair repair);
    }
}
