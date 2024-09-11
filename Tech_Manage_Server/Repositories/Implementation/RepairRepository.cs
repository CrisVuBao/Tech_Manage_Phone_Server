using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Tech_Manage_Server.Data;
using Tech_Manage_Server.DTOs;
using Tech_Manage_Server.Helpers;
using Tech_Manage_Server.Migrations;
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

            //var addRepair = new Repair
            //{
            //    DeviceName = createRepairDto.DeviceName,
            //    CreationDate = DateTime.Now,
            //    CurrentStatus = createRepairDto.Status,
            //    ErrorCondition = createRepairDto.ErrorCondition,
            //    Lend = false,
            //    IsDelete = createRepairDto.IsDelete,
            //    Note = createRepairDto.Note,
            //    ReturnDate = createRepairDto.ReturnDate,
            //    Status = "RECEVIED",
            //    TotalAmount = createRepairDto.TotalAmount,
            //    CustomerId = createRepairDto.CustomerId, // Chỉ cần tên biến Id trùng với tên biến Id của Customer là dc, CustomerId = Customer.CustomerId
            //    Customer = new Customers
            //    {
            //        Name = createRepairDto.Customer.Name,
            //        PhoneNumber = createRepairDto.Customer.PhoneNumber
            //    }

            //};

            var checkCustomer = await _dbContext.Customers.FindAsync(createRepairDto.CustomerId);
            if (checkCustomer == null)
            {
                var addRepairVip = _mapper.Map<Repair>(createRepairDto);
                addRepairVip.CreationDate = DateTime.Now;
                addRepairVip.Status = "PROGRESS";
                addRepairVip.IsDelete = false;
                addRepairVip.CustomerId = createRepairDto.CustomerId; // Chỉ cần tên biến Id trùng với tên biến Id của Customer là dc, CustomerId = Customer.CustomerId
                addRepairVip.Customer = new Customers
                {
                    Name = createRepairDto.Customer.Name,
                    PhoneNumber = createRepairDto.Customer.PhoneNumber
                };

                await _dbContext.Repairs.AddAsync(addRepairVip);
                await _dbContext.SaveChangesAsync();

                return Response<Repair>.SuccessResult("Thêm sửa chữa thành công",addRepairVip);
            }
            return Response<Repair>.Failure("Lỗi");
        }

        public async Task<List<Repair>> GetAllRepairAsync()
        {
            var getAllRepair = await _dbContext.Repairs.Join(_dbContext.Customers,
                repair => repair.CustomerId, customer => customer.CustomerId,
                (repair, customer) => new
                {
                    repair, customer
                }).Select(s => new Repair
                {
                   RepairId = s.repair.RepairId,
                   DeviceName =  s.repair.DeviceName,
                   ErrorCondition = s.repair.ErrorCondition,
                   CurrentStatus =  s.repair.CurrentStatus,
                   ImageUrl = s.repair.ImageUrl,
                   Lend = s.repair.Lend,
                   CreationDate = s.repair.CreationDate,
                   ReturnDate = s.repair.ReturnDate,
                   TotalAmount = s.repair.TotalAmount,
                   Note = s.repair.Note,
                   IsDelete = s.repair.IsDelete,
                   Status = s.repair.Status,
                   CustomerId = s.repair.CustomerId,
                   Customer = s.customer
                }).OrderByDescending(order => order.CreationDate).ToListAsync();


            
            return getAllRepair;
        }

        public async Task<Repair> GetRepairWithIdAsync(int id)
        {
            var getRepairId = await _dbContext.Repairs.FindAsync(id);
            return getRepairId;
        }

        public async Task<Repair> UpdateRepairAsync(Repair repair)
        {
            var existingRepair = await _dbContext.Repairs.FirstOrDefaultAsync(x => x.RepairId == repair.RepairId);
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
