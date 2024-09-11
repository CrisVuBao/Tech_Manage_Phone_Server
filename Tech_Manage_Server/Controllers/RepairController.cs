using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tech_Manage_Server.Data;
using Tech_Manage_Server.DTOs;
using Tech_Manage_Server.Models;
using Tech_Manage_Server.Repositories.Interface;

namespace Tech_Manage_Server.Controllers
{
    [Route("api/")]
    [ApiController]
    public class RepairController : ControllerBase
    {
        private readonly ManageDBContext _dbContext;
        private readonly IRepairRepository _repairRepository;
        private readonly IMapper _mapper;

        public RepairController(ManageDBContext dbContext, IRepairRepository repairRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _repairRepository = repairRepository;
            _mapper = mapper;
        }

        [HttpPost("CreateRepair")]
        public async Task<ActionResult<Repair>> CreateRepair([FromBody]CreateRepairDto createRepairDto)
        {
            var result = await _repairRepository.CreateRepairAsync(createRepairDto);
            return Ok(result);
        }

        [HttpGet("GetAllRepair")]
        public async Task<ActionResult<List<Repair>>> GetAllRepair()
        {
            var result = await _repairRepository.GetAllRepairAsync();
            return Ok(result);
        }

        [HttpGet("GetRepairById/{id}")]
        public async Task<ActionResult<Repair>> GetRepairById(int id) 
        {
            var result = await _repairRepository.GetRepairWithIdAsync(id);
            return Ok(result);
        }

        [HttpPut("UpdateRepair/{id}")]
        public async Task<ActionResult<Repair>> UpdateRepair(int id, UpdateRepairDto updateRepairDto)
        {
            var repair = new Repair
            {
                RepairId = id,
                DeviceName = updateRepairDto.DeviceName,
                ErrorCondition = updateRepairDto.ErrorCondition,
                CurrentStatus = updateRepairDto.CurrentStatus,
                ImageUrl = updateRepairDto.ImageUrl,
                Lend = updateRepairDto.Lend,
                CreationDate = updateRepairDto.CreationDate,
                ReturnDate = updateRepairDto.ReturnDate,
                TotalAmount = updateRepairDto.TotalAmount,
                Note = updateRepairDto.Note,
                IsDelete = updateRepairDto.IsDelete,
                Status = updateRepairDto.Status,
                CustomerId = updateRepairDto.CustomerId,
                Customer = updateRepairDto.Customer
            };

            repair = await _repairRepository.UpdateRepairAsync(repair);

            if (repair == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<Repair>(repair);
            response.Customer = repair.Customer;

            return Ok(response);
        }
    }
}
