using AutoMapper;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Tech_Manage_Server.Data;
using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.Repositories.Implementation
{
    public class RepairRepository : IRepairRepository
    {
        private readonly ManageDBContext _dbContext;
        private readonly IMapper _mapper;

        public RepairRepository(ManageDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateRepairAsync(Repair repair)
        {
            await _dbContext.Repairs.AddAsync(repair);

                //var addRepairVip = _mapper.Map<Repair>(createRepairDto);
                //addRepairVip.CreationDate = DateTime.Now;
                //addRepairVip.Status = "PROGRESS";
                //addRepairVip.IsDelete = false;
                //addRepairVip.CustomerId = createRepairDto.CustomerId; // Chỉ cần tên biến Id trùng với tên biến Id của Customer là dc, CustomerId = Customer.CustomerId
                //addRepairVip.Customer = new Customer
                //{
                //    FullName = createRepairDto.Customer.FullName,
                //    PhoneNumber = createRepairDto.Customer.PhoneNumber
                //};
        }

        public async Task<List<Repair>> GetAllRepairAsync()
        {
            var getAllRepair = await _dbContext.Repairs
                .OrderByDescending(r => r.CreationDate)
                .Include(r => r.Customer)
                .Include(i => i.RepairItems)
                    .ThenInclude(i => i.Inventory)
                .ToListAsync();


            //.Select(r => new Repair
            //{
            //    RepairId = r.RepairId,
            //    DeviceName = r.DeviceName,
            //    ErrorCondition = r.ErrorCondition,
            //    ImageUrl = r.ImageUrl,
            //    Lend = r.Lend,
            //    CreationDate = r.CreationDate,
            //    ReturnDate = r.ReturnDate,
            //    TotalAmount = r.TotalAmount,
            //    Note = r.Note,
            //    IsDelete = r.IsDelete,
            //    Status = r.Status,
            //    CustomerId = r.CustomerId,
            //    Customer = r.Customer
            //})


            return getAllRepair;
        }

        public async Task<IEnumerable<Repair>> GetRepairByCustomerIdAsync(int customerId)
        {
            var getRepairByCustomerId = await _dbContext.Repairs
                    .Where(r => r.CustomerId == customerId)
                    .OrderByDescending(r => r.CreationDate)
                    .Include(r => r.Customer)
                    .Include(i => i.RepairItems)
                        .ThenInclude(i => i.Inventory)
                    .ToListAsync();

            return getRepairByCustomerId;
        }

        public async Task<IEnumerable<Repair>> GetRepairByNumberPhone(string phoneNumber)
        {
            var getRepairByNumberPhone = await _dbContext.Repairs.Where(p => p.Customer.PhoneNumber == phoneNumber)
                .Include(r => r.Customer)
                .Include(r => r.RepairItems)
                .ToListAsync();
            return getRepairByNumberPhone;
        }

        public async Task<Repair> GetRepairWithIdAsync(int id)
        {
            var getRepairId = await _dbContext.Repairs
                .Include(i => i.Customer)
                .Include(i => i.RepairItems)
                    .ThenInclude(i => i.Inventory)
                .FirstOrDefaultAsync(r => r.RepairId == id);
            if (getRepairId == null)
            {
                throw new KeyNotFoundException("Repair not found");
            }

            return getRepairId;
        }

        public void RemoveRepair(Repair repair)
        {
            _dbContext.Repairs.Remove(repair);
        }

        public void UpdateRepairAsync(Repair repair)
        {
             _dbContext.Repairs.Update(repair);
        }

        public void UpdateStatusRepairAsync(int id)
        {
            var getRepair = _dbContext.Repairs.Find(id);

            // cập nhật status
            getRepair.Status = "COMPLETED";
            _dbContext.SaveChanges();
        }

        public void DeleteRepairById(int id)
        {
            var getById = _dbContext.Repairs.FirstOrDefault(f => f.RepairId == id);
            _dbContext.Remove(getById);
            _dbContext.SaveChanges();
        }

        public async Task<bool> UploadRepairImageFile(int repairId, string repairImageUrl)
        {
            var getRepairById = await GetRepairWithIdAsync(repairId);

            if (getRepairById == null)
            {
                getRepairById.ImageUrl = repairImageUrl;
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
