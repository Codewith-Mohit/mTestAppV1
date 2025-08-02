using AutoMapper;
using Microsoft.EntityFrameworkCore;
using mTestAppV1.Data;
using mTestAppV1.Entities;
using mTestAppV1.Mapper;
using mTestAppV1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mTestAppV1.Test
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task AddOrderToCustomerAsync_AddsOrder()
        {
            var customerId = Guid.NewGuid();
            var order = new Order { OrderId = Guid.NewGuid(), CustomerId = customerId, OrderAmount = 100 };

            var options = new DbContextOptionsBuilder<TestDB>()
               .UseInMemoryDatabase(databaseName: "Test_AddOrderToCustomer")
               .Options;

            using var context = new TestDB(options);

            context.Customers.Add(new Customer { Id = customerId, Name = "Test", Email = "test@test.com" });
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            IMapper mapper = config.CreateMapper();
            await context.SaveChangesAsync();

            var service = new OrderServices(context, mapper);

            var result = await service.AddOrderToCustomerAsync(customerId, order);

            Assert.True(result);

            Assert.Contains(context.Orders, o => o.CustomerId == customerId && o.OrderId == order.OrderId);
        }

        [Fact]
        public async Task GetCustomerOrdersAsync_ReturnsOrders()
        {
            var customerId = Guid.NewGuid();
            var orders = new List<Order>
            {
                new Order { OrderId = Guid.NewGuid(), CustomerId = customerId, OrderAmount = 100 },
                new Order { OrderId = Guid.NewGuid(), CustomerId = customerId, OrderAmount = 200 }
            };
            var options = new DbContextOptionsBuilder<TestDB>()
               .UseInMemoryDatabase(databaseName: "Test_GetCustomerOrders")
               .Options;

            using var context = new TestDB(options);

            context.Customers.Add(new Customer { Id = customerId, Name = "Test", Email = "test@test.com" });

            context.Orders.AddRange(orders);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            IMapper mapper = config.CreateMapper();

            await context.SaveChangesAsync();

            var service = new OrderServices(context, mapper);
            var result = await service.GetCustomerOrdersAsync(customerId);
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, o => Assert.Equal(customerId, o.CustomerId));
        }
    }
}
