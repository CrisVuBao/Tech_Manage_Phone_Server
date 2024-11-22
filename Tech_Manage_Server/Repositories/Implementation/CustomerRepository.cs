using Microsoft.EntityFrameworkCore;
using Tech_Manage_Server.Data;
using Tech_Manage_Server.Models;
using Tech_Manage_Server.Repositories.Interface;

namespace Tech_Manage_Server.Repositories.Implementation
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly ManageDBContext _manageDBContext;

        public CustomerRepository(ManageDBContext manageDBContext)
        {
            _manageDBContext = manageDBContext;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _manageDBContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            return await _manageDBContext.Customers.FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task<Customer> GetCustomerByPhoneNumberAsync(string phoneNumber)
        {
            return await _manageDBContext.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _manageDBContext.Customers.AddAsync(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _manageDBContext.Customers.Update(customer);
        }

        public void RemoveCustomer(Customer customer)
        {
            _manageDBContext.Customers.Remove(customer);
        }
    }
}
