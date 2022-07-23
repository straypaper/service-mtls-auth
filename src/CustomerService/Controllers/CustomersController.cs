using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger)
        {
            _logger = logger;
        }

        [HttpGet("customers")]
        [Authorize(AuthenticationSchemes = CertificateAuthenticationDefaults.AuthenticationScheme)]
        public IEnumerable<Customer> GetCustomers()
        {
            return new[] {
                new Customer("Joe"),
                new Customer("Jane")
            };
        }
    }
}