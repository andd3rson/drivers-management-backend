using Drivers_Management.Domain.Contracts.Repository;
using Drivers_Management.Domain.Contracts.Services;
using Drivers_Management.Domain.Models;
using Drivers_Management.Domain.Utils;
using FluentValidation;
using OneOf;

namespace Drivers_Management.Domain.Services
{
    public class DriverServices : IDriverServices
    {
        private readonly IDriverRepository _drivers;
        private readonly IVehicleServices _vehicleService;
        private readonly IValidator<Driver> _validator;
        public DriverServices(IDriverRepository drivers, 
                IValidator<Driver> validator, IVehicleServices vehicleService)
        {
            _drivers = drivers;
            _validator = validator;
            _vehicleService = vehicleService;
        }

        public async Task<IEnumerable<Driver>> GetAllAsync(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            return await _drivers.GetAllAsync(pageSize, pageNumber);
        }

        public async Task<(Driver, bool)> CreateAsync(Driver driver)
        {
            var validateModel = await _validator.ValidateAsync(driver);
            if (!validateModel.IsValid)
                return (driver, false);
            driver.CreatedAt = DateTime.UtcNow;
            var resquest = await _drivers.Create(driver);
            return (resquest, resquest.Id != 0);
        }

        public async Task<bool> UpdateAsync(Driver driver)
        {
            var validateModel = await _validator.ValidateAsync(driver);
            var exists = await _drivers.GetByCpfAsync(driver.Cpf);

            if (exists is null)
                return false;
            exists.ToUpdate(driver);
            return await _drivers.UpdateAsync(exists);
        }

        public async Task<Driver> GetByCpfAsync(string cpf)
        {
            return await _drivers.GetByCpfAsync(cpf);
        }

        public async Task<bool> Vinculate(int driverId, int vehicleId)
        {
            var driver = await _drivers.GetByIdAsync(driverId);
            var vehicle = await _vehicleService.GetByIdAsync(vehicleId);

            if (driver is null && vehicle is null)
            {
                return false;
            }
            driver.Vehicles.Add(vehicle);
            return await _drivers.UpdateAsync(driver);

        }
    }
}