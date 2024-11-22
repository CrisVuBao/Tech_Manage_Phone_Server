using Tech_Manage_Server.Data;
using Tech_Manage_Server.Models;
using Tech_Manage_Server.Repositories.Interface;

namespace Tech_Manage_Server.Repositories.Implementation
{
    public class RepairItemRepository : IRepairItemRepository
    {
        private readonly ManageDBContext _manageDBContext;

        public RepairItemRepository(ManageDBContext manageDBContext) {
            _manageDBContext = manageDBContext;
        }

        public async Task AddRepairItemAsync(RepairItem repairItem)
        {
            await _manageDBContext.RepairItems.AddAsync(repairItem);
        }

        public Task<RepairItem> GetRepairItemAsync(RepairItem repairItem)
        {
            throw new NotImplementedException();
        }
    }
}
