using AutoMapper;
using Microsoft.EntityFrameworkCore;
using mTestAppV1.Data;
using mTestAppV1.Dto;
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
    public class CustomerServiceTests
    {
        [Fact]
        public async Task GetCustomersAsync_ReturnsAllCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer { Id = Guid.NewGuid(), Name = "A", Email = "a@test.com" },
                new Customer { Id = Guid.NewGuid(), Name = "B", Email = "b@test.com" }
            };

            var options = new DbContextOptionsBuilder<TestDB>()
               .UseInMemoryDatabase(databaseName: "Test_GetCustomer")
               .Options;

            using var context = new TestDB(options);
            //adding sample customer for test
            context.Customers.AddRange(customers);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            IMapper mapper = config.CreateMapper();
            await context.SaveChangesAsync();

            var service = new CustomerServices(context, mapper);
            var result = await service.GetCustomersAsync();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task AddCustomerAsync_AddsCustomer()
        {
            var options = new DbContextOptionsBuilder<TestDB>()
                .UseInMemoryDatabase(databaseName: "Test_AddCustomer")
                .Options;

            using var context = new TestDB(options);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            IMapper mapper = config.CreateMapper();

            var service = new CustomerServices(context, mapper);

            var dto = new CustomerDto { Name = "Test", Email = "test@test.com" };
            var result = await service.AddCustomerAsync(dto);

            Assert.Equal("Test", result.Name);
            Assert.Single(context.Customers);
        }

        [Fact]
        public async Task DeleteCustomerAsync_RemovesCustomer()
        {
            var customerId = Guid.NewGuid();
            var customer = new Customer { Id = customerId, Name = "Test", Email = "test@test.com" };
            var customers = new List<Customer> { customer };

            var options = new DbContextOptionsBuilder<TestDB>()
               .UseInMemoryDatabase(databaseName: "Test_DeleteCustomer")
               .Options;

            using var context = new TestDB(options);

            //adding sample customer for test
            context.Customers.AddRange(customers);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            IMapper mapper = config.CreateMapper();

            await context.SaveChangesAsync();

            var service = new CustomerServices(context, mapper);
            await service.DeleteCustomerAsync(customerId);

            Assert.True(!context.Customers.Any(c => c.Id == customerId));
            Assert.Empty(context.Customers);
        }

        [Fact]
        public async Task UpdateCustomerAsync_UpdatesCustomer()
        {
            var customerId = Guid.NewGuid();
            var customer = new Customer { Id = customerId, Name = "OldName", Email = "OldEmail" };
            var customers = new List<Customer> { customer };
            var options = new DbContextOptionsBuilder<TestDB>()
               .UseInMemoryDatabase(databaseName: "Test_UpdateCustomer")
               .Options;
            using var context = new TestDB(options);

            //adding sample customer for test
            context.Customers.AddRange(customers);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            IMapper mapper = config.CreateMapper();

            await context.SaveChangesAsync();

            var service = new CustomerServices(context, mapper);

            var dto = new CustomerDto { Name = "NewName", Email = "NewEmail" };
            var updatedCustomer = await service.UpdateCustomerAsync(dto, customerId);
            Assert.Equal("NewName", updatedCustomer.Name);
            Assert.Equal("NewEmail", updatedCustomer.Email);
            Assert.Single(context.Customers);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ReturnsCustomer()
        {
            var customerId = Guid.NewGuid();
            var customer = new Customer { Id = customerId, Name = "Test", Email = "est@test.com" };
            var options = new DbContextOptionsBuilder<TestDB>()
               .UseInMemoryDatabase(databaseName: "Test_GetCustomerById")
               .Options;

            using var context = new TestDB(options);
            //adding sample customer for test
            context.Customers.Add(customer);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            IMapper mapper = config.CreateMapper();

            await context.SaveChangesAsync();
            var service = new CustomerServices(context, mapper);
            var result = await service.GetCustomerByIdAsync(customerId);
            Assert.NotNull(result);
            Assert.Equal(customerId, result.Id);
        }

    }
}
