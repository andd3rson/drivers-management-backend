using Drivers_Management.Application.Dtos;
using Drivers_Management.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
namespace Drivers_Management.Application.Controllers
{
    [ApiController]
    [Route("vehicle")]
    public class VehicleController : ControllerBase
    {
        // TODO: Implement Automapper
        private readonly IVehicleServices _vehicleServices;
        private readonly IMapper _mapper;
        public VehicleController(IVehicleServices vehicleServices, IMapper mapper)
        {
            _vehicleServices = vehicleServices;
            _mapper = mapper;
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
            var vehicleMapper = _mapper.Map<Domain.Models.Vehicle>(vehicle);
            var id = await _vehicleServices.PostAsyncllAsync(vehicleMapper);
            return Ok(id.IsT1);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync()
        {
            return NoContent();
        }

    }
}