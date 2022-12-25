using Drivers_Management.Application.Dtos;
using Drivers_Management.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Drivers_Management.Domain.Models;

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
        {
            var req = await _vehicleServices.GetAllAsync(pageNumber, pageSize);
            var list = new List<VehiclesResponse>();
            foreach (var item in req)
            {
                var s = item.Drivers.Select(x => x.Drivers);
                var t = _mapper.Map<List<DriversResponse>>(s);

                list.Add(new VehiclesResponse(item.Id, item.Brand, item.Brand, item.Year, t));
            }
            // RUNNING WITH AUTOMAPPER 
            // return Ok(_mapper.Map<List<VehiclesResponse>>(await _vehicleServices.GetAllAsync(pageNumber, pageSize)));
            return Ok(list);

        }

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
            var request = await _vehicleServices.CreateAsync(_mapper.Map<Vehicle>(vehicle));
            if (!request.Item2)
                return BadRequest();

            return Created("/", request.Item1.Id);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync()
        {
            return NoContent();
        }

    }
}