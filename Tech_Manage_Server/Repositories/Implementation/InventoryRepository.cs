using Microsoft.EntityFrameworkCore;
using Tech_Manage_Server.Data;
using Tech_Manage_Server.Models;
using Tech_Manage_Server.Repositories.Interface;

namespace Tech_Manage_Server.Repositories.Implementation
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ManageDBContext _manageDBContext;

        public InventoryRepository(ManageDBContext manageDBContext) 
        {
            _manageDBContext = manageDBContext;
        }

        public async Task<Inventory> GetInventoryByIdAsync(int inventoryId)
        {
            return await _manageDBContext.Inventorys
                .FirstOrDefaultAsync(i => i.InventoryId == inventoryId);
        }

        public async Task<IEnumerable<Inventory>> GetAllInventoriesAsync()
        {
            return await _manageDBContext.Inventorys.ToListAsync();
        }

        public async Task AddInventoryAsync(Inventory inventory)
        {
            await _manageDBContext.Inventorys.AddAsync(inventory);
        }

        public void UpdateInventory(Inventory inventory)
        {
            _manageDBContext.Inventorys.Update(inventory);
        }

        public void RemoveInventory(Inventory inventory)
        {
            _manageDBContext.Inventorys.Remove(inventory);
        }


    }
}
