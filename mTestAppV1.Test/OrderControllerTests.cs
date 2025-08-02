using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using mTestAppV1.Controllers;
using mTestAppV1.Dto;
using mTestAppV1.Entities;
using mTestAppV1.Services.Interfaces;

namespace mTestAppV1.Test
{
    public class OrderControllerTests
    {
        [Fact]
        public async Task AddOrderToCustomer_ReturnsCreated_WhenOrderAdded()
        {
            var mockService = new Mock<IOrderServices>();
            var mockCustomerService = new Mock<ICustomerServices>();            

            var customerId = Guid.NewGuid();
            mockService.Setup(o => o.AddOrderToCustomerAsync(It.IsAny<Guid>(), It.IsAny<Order>()))
                       .ReturnsAsync(true);

            var controller = new OrderController(mockService.Object, mockCustomerService.Object, null);

            var orderDto = new OrderDto { OrderAmount = 100 };
            var result = await controller.AddOrderToCustomer(customerId, orderDto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdResult.StatusCode ?? 201);
        }

        [Fact]
        public async Task GetCustomerOrders_ReturnsOk_WhenOrdersExist()
        {
            var mockService = new Mock<IOrderServices>();
            var mockCustomerService = new Mock<ICustomerServices>();
            var mockLogger = new Mock<ILogger<CustomerController>>();
            var customerId = Guid.NewGuid();
            var orders = new List<Order> { new Order { OrderId = Guid.NewGuid(), CustomerId = customerId } };
            mockCustomerService.Setup(s => s.GetCustomerByIdAsync(customerId))
                       .ReturnsAsync(new Customer { Id = customerId });
            mockService.Setup(s => s.GetCustomerOrdersAsync(customerId))
                       .ReturnsAsync(orders);

            var controller = new OrderController(mockService.Object, mockCustomerService.Object,null);

            var result = await controller.GetCustomerOrders(customerId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(orders, okResult.Value);
        }

    }
}
