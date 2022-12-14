using Drivers_Management.Application.Dtos;
using Drivers_Management.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Drivers_Management.Application.Controllers
{
    [ApiController]
    [Route("vehicle")]
    public class VehicleController : ControllerBase
    {
        // TODO: Implement Automapper
        private readonly IVehicleServices _vehicleServices;
        public VehicleController(IVehicleServices vehicleServices)
        {
            _vehicleServices = vehicleServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int pageSize = 50, [FromQuery] int pageNumber = 1)
            => Ok(await _vehicleServices.GetAllAsync(pageNumber, pageSize));

        [HttpGet("plate")]
        public async Task<IActionResult> GetByPlateAsync([FromQuery] string plate)
            => Ok(await _vehicleServices.GetByPlateAsync(plate));

        // TODO : One of the last things to implement 
        [HttpGet("filter")]
        public async Task<IActionResult> GetAdvancedFilterAsync()
            => Ok(await _vehicleServices.GetByAdvancedFilterAsync());


        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] VehiclesRequest vehicle)
        {

            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync()
        {
            return NoContent();
        }



    }
}