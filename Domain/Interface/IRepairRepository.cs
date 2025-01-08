using Tech_Manage_Server.Models;

namespace Domain.Interface
{
    public interface IRepairRepository
    {
        Task<List<Repair>> GetAllRepairAsync();
        Task<Repair> GetRepairWithIdAsync(int id);
        Task<IEnumerable<Repair>> GetRepairByCustomerIdAsync(int customerId);
        Task<IEnumerable<Repair>> GetRepairByNumberPhone(string phoneNumber );
        Task CreateRepairAsync(Repair repair);
        void UpdateRepairAsync(Repair repair);
        void UpdateStatusRepairAsync(int id);
        void RemoveRepair(Repair repair);
        void DeleteRepairById(int id);
        Task<bool> UploadRepairImageFile(int repairId, string repairImageUrl);
    }
}
