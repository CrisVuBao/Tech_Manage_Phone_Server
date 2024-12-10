using Tech_Manage_Server.Models;

namespace Domain.Interface
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
