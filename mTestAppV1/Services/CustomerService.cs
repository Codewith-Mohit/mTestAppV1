using AutoMapper;
using Microsoft.EntityFrameworkCore;
using mTestAppV1.Data;
using mTestAppV1.Dto;
using mTestAppV1.Entities;
using mTestAppV1.Services.Interfaces;

namespace mTestAppV1.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly TestDB _context;

        private readonly IMapper _mapper;

        public CustomerServices(TestDB context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }
        public async Task<Customer> GetCustomerByIdAsync(Guid? CustomerId)
        {
            if (CustomerId is null)
                throw new ArgumentNullException(nameof(CustomerId));
            return await _context.Customers.FindAsync(CustomerId) ?? throw new KeyNotFoundException("Customer not found");
        }
        public async Task<Customer> AddCustomerAsync(CustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);

            if (customer == null)
                throw new ArgumentNullException(nameof(customerDto), "Customer DTO cannot be null");
            customer.Id = Guid.NewGuid(); // Ensure a new ID is generated for the new customer

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        public async Task<Customer> UpdateCustomerAsync(CustomerDto customerDto, Guid? CustomerId)
        {
            if (CustomerId is null)
                throw new ArgumentNullException(nameof(CustomerId));

            var customer = await GetCustomerByIdAsync(CustomerId);

            if (customer == null)
                throw new KeyNotFoundException("Customer not found");
            _mapper.Map(customerDto, customer);

            await _context.SaveChangesAsync();
            return customer;

        }
        public async Task DeleteCustomerAsync(Guid? CustomerId)
        {
            if (CustomerId is null)
                throw new ArgumentNullException(nameof(CustomerId));
            var customer = await GetCustomerByIdAsync((Guid)CustomerId);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

    }
}
