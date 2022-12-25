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

        /* TODO: 
            * CHANGE TO AUTOMAPPER
            * Create a Custom page response for every class.
        */
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] int pageSize = 50, [FromQuery] int pageNumber = 1)
        {
            var r = await _driverServices.GetAllAsync(pageNumber, pageSize);

            List<DriverResponse> list = new List<DriverResponse>();
            var b = r.SelectMany(x => x.Vehicles).Select(x => x.Vehicles);
            var c = _mapper.Map<List<VehiclesResponse>>(b);

            foreach (var item in r)
            {
                var s = item.Vehicles.Select(x => x.Vehicles);
                var t = _mapper.Map<List<VehiclesResponse>>(s);


                list.Add(new DriverResponse(item.Id, item.Name, item.Cpf, item.Email, t));
            }
            return Ok(list);

        }
        // CHANGE HERE TO AUTOMAPPER AS WELL.
        // RETURN A FILTER LIST.
        [HttpGet("{cpf}")]
        public async Task<IActionResult> GetByCpfAsync(string cpf)
        {
            var result = await _driverServices.GetByCpfAsync(cpf);
            if (result is null)
                return NotFound();

            DriverResponse res = new DriverResponse(result.Id, result.Name, result.Cpf, result.Email, _mapper.Map<List<VehiclesResponse>>(result.Vehicles));

            return Ok(res);
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
        {
            bool t = await _driverServices.Vinculate(idDriver, idVehicle);

            return t ? Ok() : BadRequest();
        }

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