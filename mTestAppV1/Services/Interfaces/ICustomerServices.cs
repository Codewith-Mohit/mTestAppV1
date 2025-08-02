using mTestAppV1.Dto;
using mTestAppV1.Entities;

namespace mTestAppV1.Services.Interfaces
{
    public interface ICustomerServices
    {
        public Task<List<Customer>> GetCustomersAsync();
        public Task<Customer> GetCustomerByIdAsync(Guid? CustomerId);
        public Task<Customer> AddCustomerAsync(CustomerDto customer);
        public Task<Customer> UpdateCustomerAsync(CustomerDto customer, Guid? CustomerId);
        public Task DeleteCustomerAsync(Guid? CustomerId);      
    }
}
