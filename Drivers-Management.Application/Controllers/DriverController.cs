using Microsoft.AspNetCore.Mvc;
using Drivers_Management.Domain.Contracts.Services;
using Drivers_Management.Domain.Models;
using Drivers_Management.Application.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Drivers_Management.Domain.Utils;

namespace Drivers_Management.Application.Controllers
{
    [ApiController]
    [Authorize]
    [Route("drivers")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverServices _driverServices;
        private readonly IMapper _mapper;
        public DriverController(IDriverServices driverServices, IMapper mapper)
        {
            _driverServices = driverServices;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationQuery pagination)
        {
            var res = await _driverServices.GetAllAsync(_mapper.Map<PaginationFilter>(pagination));
            return Ok(_mapper.Map<IEnumerable<DriverResponse>>(res));
        }



        // TODO: Changed to filter cpf or name;
        [HttpGet("{cpf}")]
        public async Task<IActionResult> GetFilterByCpfAsync(string cpf)
        {
            var result = await _driverServices.GetByCpfAsync(cpf);
            return Ok(
                new ApiResponse<IEnumerable<DriverResponse>>(
                    _mapper.Map<IEnumerable<DriverResponse>>(result)));
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync(DriverRequest driverRequest)
        {
            var driverMapped = _mapper.Map<Driver>(driverRequest);

            var result = await _driverServices.CreateAsync(driverMapped);
            if (!result.Item2)
                return BadRequest();

            return Created("/", result.Item1.Id);
        }

        [HttpPost("join")]
        public async Task<IActionResult> PostVinculatAsync([FromQuery] int idDriver, [FromQuery] int idVehicle)
            => await _driverServices.Vinculate(idDriver, idVehicle) ? Ok() : BadRequest();


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