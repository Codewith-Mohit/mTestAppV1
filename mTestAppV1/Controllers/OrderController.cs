using Microsoft.AspNetCore.Mvc;
using mTestAppV1.Dto;
using mTestAppV1.Entities;
using mTestAppV1.Services;
using mTestAppV1.Services.Interfaces;

namespace mTestAppV1.Controllers
{
    public class OrderController : Controller
    {

        private readonly IOrderServices _orderService;
        private readonly ICustomerServices _customerService;

        private readonly ILogger<CustomerController> _logger;

       public OrderController(IOrderServices orderService, ICustomerServices customerService, ILogger<CustomerController> logger)
        {
            _orderService = orderService;
            _customerService = customerService;
            _logger = logger;
        }
            [HttpGet("customers/{customerId}/orders")]
        public async Task<IActionResult> GetCustomerOrders(Guid customerId)
        {
            var customer = await _orderService.GetCustomerOrdersAsync(customerId);

            if (customer is null)
            {
                return NotFound("Customer not found.");
            }

            // Retrieve orders for the specified customer
            var orders = await _orderService.GetCustomerOrdersAsync(customerId);
            if(orders is null || !orders.Any())
            {
                return NotFound("No orders found for this customer.");
            }   

            return Ok(orders);
        }

        [HttpPost("customers/{customerId}/orders")]
        public async Task<IActionResult> AddOrderToCustomer([FromRoute] Guid customerId, [FromBody] OrderDto order)
        {
            var newOrder = new Order
            {
                OrderId = Guid.NewGuid(),
                CustomerId = customerId,
                OrderAmount = order.OrderAmount,
                OrderDate = DateTime.Now
            };

            try
            {
                await _orderService.AddOrderToCustomerAsync(customerId, newOrder);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

            CreatedAtActionResult obj = CreatedAtAction(nameof(GetCustomerOrders), new { customerId = customerId }, order);

            return obj;
        }
    }
}
