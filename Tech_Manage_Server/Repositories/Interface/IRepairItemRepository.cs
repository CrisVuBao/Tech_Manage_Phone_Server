using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.Repositories.Interface
{
    public interface IRepairItemRepository
    {
        Task AddRepairItemAsync(RepairItem repairItem);
        Task<RepairItem> GetRepairItemAsync(RepairItem repairItem);
    }
}
