using Microsoft.AspNetCore.Mvc;
using Drivers_Management.Domain.Contracts.Services;
using Drivers_Management.Domain.Models;
using Drivers_Management.Application.Dtos;
using AutoMapper;
using Drivers_Management.Application.Helpers;
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
        private IUriServices _uriServices;
        public DriverController(IDriverServices driverServices, IMapper mapper, IUriServices uriServices)
        {
            _driverServices = driverServices;
            _mapper = mapper;
            _uriServices = uriServices;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] PaginationQuery pagination)
        {
            var paginationFilter = _mapper.Map<PaginationFilter>(pagination);
            var res = await _driverServices.GetAllAsync(paginationFilter);
            var pagedReponse = PaginationHelpers
                .CreatePagedReponse<Driver>(res.Item1.ToList(), paginationFilter, res.Item2, _uriServices, Request.Path.Value);
            // return Ok(_mapper.Map<IEnumerable<DriverResponse>>(res));
            return Ok(pagedReponse);
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