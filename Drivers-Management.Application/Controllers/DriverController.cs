using Microsoft.AspNetCore.Mvc;
using Drivers_Management.Domain.Contracts.Services;
using Drivers_Management.Domain.Models;

namespace Drivers_Management.Application.Controllers
{
    [ApiController]
    [Route("drivers")]
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
            var result = await _driverServices.AddAsync(driver);
            return Ok();
        }

        /* TODO: 
            * Create a pagination filter class to received pageSize and pageNumber params.
            * Create a Custom page response for every class.

        */
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize = 50, [FromQuery] int pageNumber = 1)
        {
            return Ok(await _driverServices.GetAllAsync(pageNumber, pageSize));
        }
    }
}