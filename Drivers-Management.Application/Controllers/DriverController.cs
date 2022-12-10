using Microsoft.AspNetCore.Mvc;
using Drivers_Management.Domain.Contracts.Services;
using Drivers_Management.Domain.Models;

namespace Drivers_Management.Application.Controllers
{
    [ApiController]
    [Route("driver")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverServices _driverServices;

        public DriverController(IDriverServices driverServices)
        {
            _driverServices = driverServices;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Driver driver)
        {
            return Ok(await _driverServices.AddAsync(driver));
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _driverServices.GetAllAsync());
        }
    }
}