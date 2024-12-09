using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.Repositories.Interface
{
    public interface IRepairItemRepository
    {
        Task<RepairItem> GetRepairItemByIdAsync(int repairItemId);
        Task<IEnumerable<RepairItem>> GetRepairItemsByRepairIdAsync(int repairId);
        Task AddRepairItemAsync(RepairItem repairItem);
        void UpdateRepairItem(RepairItem repairItem);
        void RemoveRepairItem(RepairItem repairItem);
    }
}
