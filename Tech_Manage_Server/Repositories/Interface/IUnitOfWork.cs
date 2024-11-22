using Microsoft.EntityFrameworkCore.Storage;

namespace Tech_Manage_Server.Repositories.Interface
{
    public interface IUnitOfWork
    {
        // Các repository khác...
        IRepairItemRepository RepairItems { get; }
        ICustomerRepository Customers { get; }
        IInventoryRepository Inventories { get; }

        Task<int> CompleteAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
