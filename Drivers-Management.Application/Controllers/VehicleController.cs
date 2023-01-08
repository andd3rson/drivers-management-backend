using Drivers_Management.Application.Dtos;
using Drivers_Management.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Drivers_Management.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace Drivers_Management.Application.Controllers
{
    [ApiController]
    [Authorize]
    [Route("vehicle")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleServices _vehicleServices;
        private readonly IMapper _mapper;
        public VehicleController(IVehicleServices vehicleServices, IMapper mapper)
        {
            _vehicleServices = vehicleServices;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int pageSize = 50, [FromQuery] int pageNumber = 1)
            => Ok(_mapper.Map<List<VehiclesResponse>>(await _vehicleServices.GetAllAsync(pageNumber, pageSize)));


        [HttpGet("plate")]
        public async Task<IActionResult> GetByPlateAsync([FromQuery] string plate)
            => Ok(
                _mapper.Map<VehiclesResponse>(await _vehicleServices.GetByPlateAsync(plate))
                );

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] VehiclesRequest vehicle)
        {
            var request = await _vehicleServices.CreateAsync(_mapper.Map<Vehicle>(vehicle));
            if (!request.Item2)
                return BadRequest();

            return Created("/", request.Item1.Id);
        }

    }
}