using mTestAppV1.Entities;

namespace mTestAppV1.Services.Interfaces
{
    public interface IOrderServices
    {
        public Task<bool> AddOrderToCustomerAsync(Guid customerId, Order order);
        public Task<List<Order>> GetCustomerOrdersAsync(Guid customerId);
    }
}
