using Tech_Manage_Server.DTOs.RepairModelDto;
using Tech_Manage_Server.Helpers;
using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.Repositories.Interface
{
    public interface IRepairRepository
    {
        Task<List<Repair>> GetAllRepairAsync();
        Task<Repair> GetRepairWithIdAsync(int id);
        Task<IEnumerable<Repair>> GetOrdersByCustomerIdAsync(int customerId);
        Task CreateRepairAsync(Repair repair);
        void UpdateRepairAsync(Repair repair);
        void RemoveRepair(Repair repair);
    }
}
