using Tech_Manage_Server.DTOs;
using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.Repositories.Interface
{
    public interface IRepairRepository
    {
        Task<List<Repair>> GetAllRepairAsync();
        Task<Repair> GetRepairWithIdAsync(int id);
        Task<Repair> CreateRepairAsync(CreateRepairDto repairDto);
        Task<Repair> UpdateRepairAsync(int repairId, RepairDto repairDto);
    }
}
