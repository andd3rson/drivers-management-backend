using Drivers_Management.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Drivers_Management.Application.Controllers
{
    [ApiController]
    [Route("vehicle")]
    public class VehicleController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok();
        }
        [HttpGet("plate")]
        public async Task<IActionResult> GetByPlateAsync([FromQuery] string plate)
        {
            
            return Ok();
        }
        // TODO : One of the last things to implement 
        [HttpGet("filter")]
        public async Task<IActionResult> GetAdvancedFilterAsync()
        {
            return Ok();
        }

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