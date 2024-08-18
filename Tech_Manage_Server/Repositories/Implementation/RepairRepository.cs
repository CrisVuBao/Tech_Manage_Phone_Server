using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tech_Manage_Server.Data;
using Tech_Manage_Server.DTOs;
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

        public async Task<Repair> CreateRepairAsync(CreateRepairDto repairDto)
        {
            var addRepair = _mapper.Map<Repair>(repairDto);
            addRepair.CreationDate = DateTime.Now;
            addRepair.Status = "RECEVIED";
            addRepair.IsDelete = false;

            await _dbContext.Repairs.AddAsync(addRepair);
            await _dbContext.SaveChangesAsync();

            return addRepair;
        }

        public async Task<List<Repair>> GetAllRepairAsync()
        {
            var getAllRepair = await _dbContext.Repairs.FromSqlRaw("GetAllRepairCustomers").ToListAsync();
            return getAllRepair;
        }

        public async Task<Repair> GetRepairWithIdAsync(int id)
        {
            var getRepairId = await _dbContext.Repairs.FindAsync(id);
            return getRepairId;
        }

        public async Task<Repair> UpdateRepairAsync(int repairId, RepairDto repairDto)
        {

            var updateRepair = _mapper.Map<Repair>(repairDto);
            return updateRepair;
        }
    }
}
