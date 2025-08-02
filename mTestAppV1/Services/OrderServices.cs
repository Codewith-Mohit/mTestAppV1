using AutoMapper;
using Microsoft.EntityFrameworkCore;
using mTestAppV1.Data;
using mTestAppV1.Entities;
using mTestAppV1.Services.Interfaces;

namespace mTestAppV1.Services
{
    public class OrderServices : IOrderServices
    {

        private readonly TestDB _context;
        private readonly IMapper _mapper;

        public OrderServices(TestDB context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddOrderToCustomerAsync(Guid customerId, Order order)
        {
            // Ensure the customer exists
            var customerExists = await _context.Customers.AnyAsync(c => c.Id == customerId);
            if (!customerExists)
                throw new Exception("Customer not found.");

            // Set the foreign key
            order.CustomerId = customerId;

            // Add the order directly to the Orders DbSet
            _context.Orders.Add(order);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Order>> GetCustomerOrdersAsync(Guid customerId)
        {
            // Ensure the customer exists
            var customerExists = await _context.Customers.AnyAsync(c => c.Id == customerId);
            if (!customerExists)
                throw new Exception("Customer not found.");
            // Retrieve orders for the specified customer
            return await _context.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
        }
    }
}
