using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tech_Manage_Server.Data;
using Tech_Manage_Server.DTOs.RepairModelDto;
using Tech_Manage_Server.Models;
using Tech_Manage_Server.Repositories.Interface;

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

        public RepairController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager,  ManageDBContext dbContext, IRepairRepository repairRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _dbContext = dbContext;
            _repairRepository = repairRepository;
            _mapper = mapper;
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

                    if (createRepairDto.CreateAccount)
                    {
                        // Tạo tài khoản cho khách hàng
                        var user = new ApplicationUser
                        {
                            PhoneNumber = createRepairDto.PhoneNumber
                        };

                        var result = await _userManager.CreateAsync(user, createRepairDto.Password);

                        if (!result.Succeeded)
                        {
                            return BadRequest(result.Errors);
                        }

                        // Liên kết với Customer
                        customer.UserId = user.Id;
                    }

                    await _unitOfWork.Customers.AddCustomerAsync(customer);
                    await _unitOfWork.CompleteAsync();
                }



            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Log lỗi nếu cần
                return StatusCode(500, "Internal server error.");
            }
            return Ok(createRepairDto);

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
