using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Tech_Manage_Server.Data;
using Tech_Manage_Server.DTOs.RepairModelDto;
using Tech_Manage_Server.Helpers;
using Tech_Manage_Server.Models;
using Tech_Manage_Server.Repositories.Interface;

namespace Tech_Manage_Server.Repositories.Implementation
{
    public class RepairRepository : IRepairRepository
    {
        private readonly ManageDBContext _dbContext;
        private readonly IMapper _mapper;

        public RepairRepository(ManageDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Response<Repair>> CreateRepairAsync(CreateRepairDto createRepairDto)
        {
            var checkCustomer = await _dbContext.Customers.FindAsync(createRepairDto); // gốc là createRepairDto.CustomerId
            if (checkCustomer == null)
            {
                var addRepairVip = _mapper.Map<Repair>(createRepairDto);
                addRepairVip.CreationDate = DateTime.Now;
                addRepairVip.Status = "PROGRESS";
                addRepairVip.IsDelete = false;
                //addRepairVip.CustomerId = createRepairDto.CustomerId; // Chỉ cần tên biến Id trùng với tên biến Id của Customer là dc, CustomerId = Customer.CustomerId
                //addRepairVip.Customer = new Customer
                //{
                //    FullName = createRepairDto.Customer.FullName,
                //    PhoneNumber = createRepairDto.Customer.PhoneNumber
                //};

                await _dbContext.Repairs.AddAsync(addRepairVip);
                await _dbContext.SaveChangesAsync();

                return Response<Repair>.SuccessResult("Thêm sửa chữa thành công", addRepairVip);
            }
            return Response<Repair>.Failure("Lỗi");
        }

        public async Task<List<Repair>> GetAllRepairAsync()
        {
            var getAllRepair = await _dbContext.Repairs
                .Include(r => r.Customer)
                .OrderByDescending(r => r.CreationDate)
                .Select(r => new Repair
                {
                    RepairId = r.RepairId,
                    DeviceName = r.DeviceName,
                    ErrorCondition = r.ErrorCondition,
                    ImageUrl = r.ImageUrl,
                    Lend = r.Lend,
                    CreationDate = r.CreationDate,
                    ReturnDate = r.ReturnDate,
                    TotalAmount = r.TotalAmount,
                    Note = r.Note,
                    IsDelete = r.IsDelete,
                    Status = r.Status,
                    CustomerId = r.CustomerId,
                    Customer = r.Customer
                })
                .ToListAsync();

            return getAllRepair;
        }

        public async Task<Repair> GetRepairWithIdAsync(int id)
        {
            var getRepairId = await _dbContext.Repairs
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(r => r.RepairId == id);
            if (getRepairId == null)
            {
                throw new KeyNotFoundException("Repair not found");
            }
            return getRepairId;
        }

        public async Task<Repair> UpdateRepairAsync(Repair repair)
        {
            var existingRepair = await _dbContext.Repairs.Include(i => i.Customer)
            .FirstOrDefaultAsync(x => x.RepairId == repair.RepairId);
            if (existingRepair != null)
            {


                _dbContext.Update(existingRepair);
                await _dbContext.SaveChangesAsync();
                return existingRepair;
            }

            return null;
        }
    }
}
