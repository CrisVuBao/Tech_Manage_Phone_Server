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

        public RepairController(ManageDBContext dbContext, IRepairRepository repairRepository)
        {
            _dbContext = dbContext;
            _repairRepository = repairRepository;
        }

        [HttpPost("CreateRepair")]
        public async Task<ActionResult<Repair>> CreateRepair(CreateRepairDto repairDto)
        {
            var result = await _repairRepository.CreateRepairAsync(repairDto);
            return Ok(result);
        }

        [HttpGet("GetAllRepair")]
        public async Task<ActionResult<List<Repair>>> GetAllRepair()
        {
            var result = await _repairRepository.GetAllRepairAsync();
            return Ok(result);
        }

        [HttpGet("GetRepairById")]
        public async Task<ActionResult<Repair>> GetRepairById(int id) 
        {
            var result = await _repairRepository.GetRepairWithIdAsync(id);
            return Ok(result);
        }
    }
}
