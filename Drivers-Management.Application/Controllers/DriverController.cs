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

        /* TODO: 
            * Create a pagination filter class to received pageSize and pageNumber params.
            * Create a Custom page response for every class.
        */
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] int pageSize = 50, [FromQuery] int pageNumber = 1)
        {
            return Ok(await _driverServices.GetAllAsync(pageNumber, pageSize));
        }

        [HttpGet("{cpf}")]
        public async Task<IActionResult> GetByCpfAsync(string cpf)
        {
            var result = await _driverServices.GetByCpfAsync(cpf);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        // TODO: Change return to 201 returns
        [HttpPost]
        public async Task<IActionResult> PostAsync(Driver driver)
        {
            var result = await _driverServices.AddAsync(driver);
            if (result.IsT0)
                return BadRequest(result.AsT0.Data);
            return Ok(result.Value);
        }

        [HttpPost("join")]
        public async Task<IActionResult> PostVinculatAsync([FromQuery] int idDriver, [FromQuery] int idVehicle)
        {
            bool t = await _driverServices.Vinculate(idDriver, idVehicle);

            return Ok(new
            {
                idDriver,
                idVehicle
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Driver driver)
        {
            if (id != driver.Id)
                return BadRequest();
            var result = await _driverServices.UpdateAsync(driver);
            return result ? NoContent() : BadRequest();
        }


    }
}