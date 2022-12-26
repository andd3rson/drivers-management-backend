using Microsoft.AspNetCore.Mvc;
using Drivers_Management.Domain.Contracts.Services;
using Drivers_Management.Domain.Models;
using Drivers_Management.Application.Dtos;
using AutoMapper;

namespace Drivers_Management.Application.Controllers
{
    [ApiController]
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

        // TODO: Create a Custom page response for every class.

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] int pageSize = 50, [FromQuery] int pageNumber = 1)
            => Ok(_mapper.Map<IEnumerable<DriverResponse>>(await _driverServices.GetAllAsync(pageNumber, pageSize)));



        // RETURN A FILTER LIST.
        [HttpGet("{cpf}")]
        public async Task<IActionResult> GetByCpfAsync(string cpf)
        {
            var result = await _driverServices.GetByCpfAsync(cpf);
            if (result is null)
                return NotFound();
            return Ok(_mapper.Map<DriverResponse>(result));
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


        // Apply tests
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