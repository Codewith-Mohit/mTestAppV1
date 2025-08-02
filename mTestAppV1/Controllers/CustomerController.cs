using Microsoft.AspNetCore.Mvc;
using mTestAppV1.Dto;
using mTestAppV1.Entities;
using mTestAppV1.Services.Interfaces;

namespace mTestAppV1.Controllers
{
    public class CustomerController : Controller
    {

        private readonly ICustomerServices _customerService;

        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerServices customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpGet("customers")]
        public async Task<List<Customer>> GetCustomers()
        {
            _logger.LogInformation("Fetching all customers...");
            var customers = await _customerService.GetCustomersAsync();
            _logger.LogInformation($"Total customers fetched: {customers.Count}");
            return customers;
        }

        [HttpGet("customer/{id}")]
        public async Task<Customer?> GetCustomer(Guid id)
        {
            _logger.LogInformation($"Fetching customer with ID: {id}");
            var customer = await _customerService.GetCustomerByIdAsync(id);
            _logger.LogInformation(customer != null
                ? $"Customer found: {customer.Name}"
                : "Customer not found.");
            return customer;
        }

        [HttpPost("customer")]
        public async Task<IActionResult> CreateCustomer(CustomerDto objCustomer)
        {
            _logger.LogInformation("Creating a new customer...");
            var objcustomer = await _customerService.AddCustomerAsync(objCustomer);

            if (objcustomer == null)
            {
                _logger.LogError("Customer creation failed.");
                return BadRequest("Customer creation failed.");
            }

            CreatedAtActionResult obj = CreatedAtAction(nameof(GetCustomer), new { id = objcustomer.Id }, objcustomer);
            
            _logger.LogInformation($"Customer created with ID: {objcustomer.Id}");
            if (obj.StatusCode is 201)
                return obj;
            else
                return BadRequest("Product creation not succeded.");

        }

        [HttpPut("customers/{id}")]
        public async Task<IActionResult> Updatecustomers(CustomerDto objCustomer, Guid id)
        {
            return Ok(await _customerService.UpdateCustomerAsync(objCustomer, id));
        }

        [HttpDelete("customers/{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return Ok(); // 204 No Content
        }       
    }
}
