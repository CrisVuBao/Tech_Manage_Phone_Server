using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Tech_Manage_Server.Data;
using Tech_Manage_Server.Models;
using Tech_Manage_Server.Repositories.Interface;

namespace Tech_Manage_Server.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ManageDBContext _manageDBContext;

        public IRepairItemRepository RepairItems { get; private set; }
        public ICustomerRepository Customers { get; private set; }

        public IInventoryRepository Inventories { get; private set; }

        public UnitOfWork(ManageDBContext manageDBContext) 
        {
            _manageDBContext = manageDBContext;
            RepairItems = new RepairItemRepository(_manageDBContext);
            Customers = new CustomerRepository(_manageDBContext);
            Inventories = new InventoryRepository(_manageDBContext);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _manageDBContext.Database.BeginTransactionAsync();
        }

        public async Task<int> CompleteAsync()
        {
            return await _manageDBContext.SaveChangesAsync();
        }

        // dùng throw new NotImplementedException(); nếu ko triển khai method
    }
}
