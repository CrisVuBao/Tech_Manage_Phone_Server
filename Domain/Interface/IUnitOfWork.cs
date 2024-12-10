using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Interface
{
    public interface IUnitOfWork
    {
        // Các repository khác...
        IRepairRepository Repairs { get; }
        IRepairItemRepository RepairItems { get; }
        ICustomerRepository Customers { get; }
        IInventoryRepository Inventories { get; }

        Task CompleteAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
