using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using mTestAppV1.Controllers;
using mTestAppV1.Dto;
using mTestAppV1.Entities;
using mTestAppV1.Services.Interfaces;

namespace mTestAppV1.Test
{
    public class CustomerControllerTests
    {
        [Fact]
        public async Task GetCustomer_ReturnsCustomer_WhenCustomerExists()
        {
            // Arrange
            var mockService = new Mock<ICustomerServices>();
            var mockLogger = new Mock<ILogger<CustomerController>>();
            var customerId = Guid.NewGuid();
            mockService.Setup(s => s.GetCustomerByIdAsync(customerId))
                       .ReturnsAsync(new Customer { Id = customerId, Name = "Test", Email = "test@test.com" });

            var controller = new CustomerController(mockService.Object, mockLogger.Object);

            // Act
            var result = await controller.GetCustomer(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.Id);
        }        
    }
}