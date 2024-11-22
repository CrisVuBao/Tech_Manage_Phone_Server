using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.Repositories.Interface
{
    public interface IInventoryRepository
    {
        Task<Inventory> GetInventoryByIdAsync(int inventoryId);
        Task<IEnumerable<Inventory>> GetAllInventoriesAsync();
        Task AddInventoryAsync(Inventory inventory);
        void UpdateInventory(Inventory inventory);
        void RemoveInventory(Inventory inventory);
    }
}
