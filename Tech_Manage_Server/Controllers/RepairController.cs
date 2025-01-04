using AutoMapper;
using Domain.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tech_Manage_Server.Data;
using Tech_Manage_Server.DTOs.RepairModelDto;
using Tech_Manage_Server.Models;

namespace Tech_Manage_Server.Controllers
{
    [Route("api/")]
    [ApiController]
    public class RepairController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ManageDBContext _dbContext;
        private readonly IRepairRepository _repairRepository;
        private readonly IMapper _mapper;

        public RepairController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, ManageDBContext dbContext, IRepairRepository repairRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _dbContext = dbContext;
            _repairRepository = repairRepository;
            _mapper = mapper;
        }

        [HttpGet("GetRepairById/{id}")]
        public async Task<ActionResult> GetRepairById(int id)
        {
            var repair = await _unitOfWork.Repairs.GetRepairWithIdAsync(id);
            var repairMap = _mapper.Map<RepairDto>(repair);

            if (repair == null)
            {
                return NotFound($"Phiếu sửa chữa với ID {id} không tìm thấy.");
            }
            return Ok(repairMap);
        }

        [HttpPost("CreateRepair")]
        public async Task<ActionResult<Repair>> CreateRepair([FromBody] CreateRepairDto createRepairDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                Customer customer;

                // Kiểm tra xem khách hàng đã tồn tại chưa dựa trên số điện thoại
                customer = await _unitOfWork.Customers.GetCustomerByPhoneNumberAsync(createRepairDto.PhoneNumber);

                if (customer != null)
                {
                    // Khách hàng đã tồn tại
                    // Kiểm tra xem khách hàng có tài khoản hay không
                    bool hasAccount = !string.IsNullOrEmpty(customer.UserId.ToString());
                    // Có thể sử dụng thông tin này để hiển thị cho Admin
                }
                else
                {
                    // Tạo mới Customer
                    customer = new Customer
                    {
                        FullName = createRepairDto.FullName,
                        PhoneNumber = createRepairDto.PhoneNumber,
                        Address = createRepairDto.Address,
                        CreatedAt = DateTime.UtcNow
                    };

                    //if (createRepairDto.CreateAccount)
                    //{
                    //    // Tạo tài khoản cho khách hàng
                    //    var user = new ApplicationUser
                    //    {
                    //        UserName = createRepairDto.Email,
                    //        Email = createRepairDto.Email,
                    //        PhoneNumber = createRepairDto.PhoneNumber
                    //    };

                    //    var result = await _userManager.CreateAsync(user, createRepairDto.Password);

                    //    if (!result.Succeeded)
                    //    {
                    //        return BadRequest(result.Errors);
                    //    }

                    //    // Liên kết với Customer
                    //    customer.UserId = user.Id;
                    //}

                    await _unitOfWork.Customers.AddCustomerAsync(customer);
                    await _unitOfWork.CompleteAsync();
                }

                // Tạo Repair
                var repair = _mapper.Map<Repair>(createRepairDto);
                repair.CustomerId = customer.CustomerId;
                repair.Status = "PROGRESS";
                repair.CreationDate = DateTime.UtcNow;
                repair.IsDelete = false;

                await _unitOfWork.Repairs.CreateRepairAsync(repair);
                await _unitOfWork.CompleteAsync();

                //// Xử lý RepairItem (nếu có)
                //if (createRepairDto != null && createRepairDto.RepairItems.Any())
                //{
                //    foreach (var itemDto in createRepairDto.RepairItems)
                //    {
                //        // Kiểm tra tồn kho
                //        var inventoryItem = await _unitOfWork.Inventories.GetInventoryByIdAsync(itemDto.InventoryId);
                //        if (inventoryItem == null)
                //        {
                //            return BadRequest($"Linh kiện trong kho với ID {itemDto.InventoryId} không tìm thấy.");
                //        }

                //        if (inventoryItem.QuantityInStock < itemDto.Quantity)
                //        {
                //            return BadRequest($"Không đủ hàng cho mặt hàng '{inventoryItem.InventoryName}'. Số lượng có sẵn: {inventoryItem.QuantityInStock}, Số linh kiện yêu cầu: {itemDto.Quantity}.");
                //        }

                //        // Tạo RepairItem
                //        var repairItem = _mapper.Map<RepairItem>(itemDto);
                //        repairItem.RepairId = repair.RepairId;
                //        repairItem.Price = inventoryItem.Price;

                //        await _unitOfWork.RepairItems.AddRepairItemAsync(repairItem);

                //        // Cập nhật số lượng tồn kho
                //        inventoryItem.QuantityInStock -= itemDto.Quantity;
                //        _unitOfWork.Inventories.UpdateInventory(inventoryItem);
                //    }

                //    await _unitOfWork.CompleteAsync();
                //}

                // Tính tổng số tiền
                //decimal totalAmount = CalculateTotalAmount(createRepairDto);

                var repairDto = _mapper.Map<RepairDto>(repair);
                //var repairDto = new RepairDto
                //{
                //    DeviceName = repair.DeviceName,
                //    CreationDate = DateTime.Now,
                //    ErrorCondition = repair.ErrorCondition,
                //    Feedbacks = repair.Feedbacks,
                //    Employee = repair.Employee
                //};
                await transaction.CommitAsync();
                //await _unitOfWork.CompleteAsync();
                return CreatedAtAction(nameof(GetRepairById), new { id = repair.RepairId }, repairDto);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Log lỗi nếu cần
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("GetAllRepair")]
        public async Task<ActionResult<List<Repair>>> GetAllRepair()
        {
            var result = await _repairRepository.GetAllRepairAsync();
            var repairMap = _mapper.Map<List<RepairDto>>(result);
            return Ok(repairMap);
        }


        [HttpPut("UpdateRepair/{id}")]
        public async Task<ActionResult<Repair>> UpdateRepair(int id, UpdateRepairDto updateRepairDto)
        {

            return Ok();
        }

        [HttpPut("UpdateStatusRepair/{id}")]
        public ActionResult UpdateStatusRepair(int id)
        {
            _repairRepository.UpdateStatusRepairAsync(id);
            _unitOfWork.CompleteAsync();
            return Ok();
        }

        //private decimal CalculateTotalAmount(CreateRepairDto createRepairDto)
        //{
        //    decimal total = 0;

        //    // Tính tổng tiền cho các linh kiện
        //    if (createRepairDto != null && createRepairDto.RepairItems.Any())
        //    {
        //        foreach (var item in createRepairDto.RepairItems)
        //        {
        //            var inventory = _unitOfWork.Inventories.GetInventoryByIdAsync(item.InventoryId).Result;
        //            if (inventory != null)
        //            {
        //                total += inventory.Price * item.Quantity;
        //            }
        //        }
        //    }

        //    return total;
        //}
    }

}
