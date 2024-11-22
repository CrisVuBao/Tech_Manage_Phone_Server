using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tech_Manage_Server.DTOs.CustomerModelDto;
using Tech_Manage_Server.Models;
using Tech_Manage_Server.Repositories.Interface;

namespace Tech_Manage_Server.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Customer
        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _unitOfWork.Customers.GetAllCustomersAsync();
            var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return Ok(customerDtos);
        }

        // GET: api/Customer/by-phone?phoneNumber={phoneNumber}
        [HttpGet("GetCustomerByPhoneNumber")]
        public async Task<IActionResult> GetCustomerByPhoneNumber([FromQuery] string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest("Phone number is required.");
            }

            var customer = await _unitOfWork.Customers.GetCustomerByPhoneNumberAsync(phoneNumber);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            var customerDto = _mapper.Map<CustomerDto>(customer);
            return Ok(customerDto);
        }

        // POST: api/Customer
        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCustomer = await _unitOfWork.Customers.GetCustomerByPhoneNumberAsync(createDto.PhoneNumber);
            if (existingCustomer != null)
            {
                return Conflict("A customer with the same phone number already exists.");
            }

            var customer = _mapper.Map<Customer>(createDto);
            customer.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Customers.AddCustomerAsync(customer);
            await _unitOfWork.CompleteAsync();

            var customerDto = _mapper.Map<CustomerDto>(customer);
            return CreatedAtAction(nameof(GetCustomerByPhoneNumber), new { phoneNumber = customer.PhoneNumber }, customerDto);
        }

        // PUT: api/Customer/{id}
        [HttpPut("UpdateCustomer/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _unitOfWork.Customers.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            _mapper.Map(updateDto, customer);
            _unitOfWork.Customers.UpdateCustomer(customer);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE: api/Customer/{id}
        [HttpDelete("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _unitOfWork.Customers.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            _unitOfWork.Customers.RemoveCustomer(customer);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
