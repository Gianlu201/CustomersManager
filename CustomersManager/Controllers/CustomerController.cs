using CustomersManager.Models;
using CustomersManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomersManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            var result = await _customerService.CreateCustomerAsync(customer);

            return result
                ? Ok(new { message = "Customer created" })
                : BadRequest(new { message = "Something went wrong" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _customerService.GetCustomersAsync();

            return result != null
                ? Ok(new { message = "Customers found", customers = result })
                : BadRequest(new { message = "Something went wrong" });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _customerService.GetCustomerByIdAsync(id);

            return result != null
                ? Ok(new { message = "Customer found", customers = result })
                : BadRequest(new { message = "Something went wrong" });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customerService.DeleteCustomerByIdAsync(id);

            return result
                ? Ok(new { message = "Customers deleted" })
                : BadRequest(new { message = "Something went wrong" });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] int id, [FromBody] Customer customer)
        {
            var result = await _customerService.UpdateCustomerAsync(id, customer);

            return result
                ? Ok(new { message = "Customer updated" })
                : BadRequest(new { message = "Something went wrong" });
        }
    }
}
