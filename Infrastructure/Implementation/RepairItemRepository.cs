using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Tech_Manage_Server.Data;
using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.Repositories.Implementation
{
    public class RepairItemRepository : IRepairItemRepository
    {
        private readonly ManageDBContext _manageDBContext;

        public RepairItemRepository(ManageDBContext manageDBContext) {
            _manageDBContext = manageDBContext;
        }

        public async Task<RepairItem> GetRepairItemByIdAsync(int repairItemId)
        {
            return await _manageDBContext.RepairItems
                    .Include(oi => oi.Inventory)
                    .FirstOrDefaultAsync(oi => oi.RepairItemId == repairItemId);
        }

        public async Task<IEnumerable<RepairItem>> GetRepairItemsByRepairIdAsync(int repairId)
        {
            return await _manageDBContext.RepairItems
                    .Where(oi => oi.RepairId == repairId)
                    .Include(oi => oi.Inventory)
                    .ToListAsync();
        }

        public async Task AddRepairItemAsync(RepairItem repairItem)
        {
            await _manageDBContext.RepairItems.AddAsync(repairItem);
        }

        public void UpdateRepairItem(RepairItem repairItem)
        {
            _manageDBContext.RepairItems.Update(repairItem);
        }

        public void RemoveRepairItem(RepairItem repairItem)
        {
            _manageDBContext.RepairItems.Remove(repairItem);
        }
    }
}
