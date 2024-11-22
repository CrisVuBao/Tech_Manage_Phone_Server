using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tech_Manage_Server.DTOs.InventoryModelDto;
using Tech_Manage_Server.Models;
using Tech_Manage_Server.Repositories.Interface;

namespace Tech_Manage_Server.Controllers
{
    [Route("api/")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InventoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Inventory/{id}
        [HttpGet("GetInventory/{id}")]
        public async Task<IActionResult> GetInventory(int id)
        {
            var inventory = await _unitOfWork.Inventories.GetInventoryByIdAsync(id);
            if (inventory == null)
            {
                return NotFound($"Inventory with ID {id} not found.");
            }
            var inventoryDto = _mapper.Map<InventoryDto>(inventory);
            return Ok(inventoryDto);
        }

        // POST: api/Inventory
        [HttpPost("CreateInventory")]
        public async Task<IActionResult> CreateInventory([FromBody] CreateInventoryDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inventory = _mapper.Map<Inventory>(createDto);
            await _unitOfWork.Inventories.AddInventoryAsync(inventory);
            await _unitOfWork.CompleteAsync();

            var inventoryDto = _mapper.Map<InventoryDto>(inventory);
            return CreatedAtAction(nameof(GetInventory), new { id = inventory.InventoryId }, inventoryDto);
        }

        // PUT: api/Inventory/{id}
        [HttpPut("UpdateInventory/{id}")]
        public async Task<IActionResult> UpdateInventory(int id, [FromBody] UpdateInventoryDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inventory = await _unitOfWork.Inventories.GetInventoryByIdAsync(id);
            if (inventory == null)
            {
                return NotFound($"Inventory with ID {id} not found.");
            }

            _mapper.Map(updateDto, inventory);
            _unitOfWork.Inventories.UpdateInventory(inventory);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE: api/Inventory/{id}
        [HttpDelete("DeleteInventory/{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            var inventory = await _unitOfWork.Inventories.GetInventoryByIdAsync(id);
            if (inventory == null)
            {
                return NotFound($"Inventory with ID {id} not found.");
            }

            _unitOfWork.Inventories.RemoveInventory(inventory);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
