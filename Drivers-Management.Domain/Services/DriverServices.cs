using Drivers_Management.Domain.Contracts.Repository;
using Drivers_Management.Domain.Contracts.Services;
using Drivers_Management.Domain.Models;
using Drivers_Management.Domain.Utils;
using FluentValidation;

namespace Drivers_Management.Domain.Services
{
    public class DriverServices : IDriverServices
    {
        private readonly IDriverRepository _drivers;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IValidator<Driver> _validator;
        public DriverServices(IDriverRepository drivers, 
                IValidator<Driver> validator, IVehicleRepository vehicleRepository)
        {
            _drivers = drivers;
            _validator = validator;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<(IEnumerable<Driver>, int)> GetAllAsync(PaginationFilter pagination)
        {
            var res =  await _drivers.GetAllAsync(pagination.PageSize, pagination.PageNumber);
            var countedPage = await _drivers.CountAsync();
          
            
            return (res, countedPage);
        }
        public async Task<IEnumerable<Driver>> GetByCpfAsync(string cpf)
        {
            return await _drivers.GetByCpfAsync(cpf);
        }

        // TODO: Add uniq cpf validate
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
            if(!validateModel.IsValid)
                return false;

            var exists = await _drivers.GetByIdAsync(driver.Id);

            if (exists is null)
                return false;

            exists.ToUpdate(driver);
            return await _drivers.UpdateAsync(exists);
        }


        public async Task<bool> Vinculate(int driverId, int vehicleId)
        {
            var driver = await _drivers.GetByIdAsync(driverId);
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);

            if (driver is null && vehicle is null)
            {
                return false;
            }
            driver.Vehicles.Add(vehicle);
            return await _drivers.UpdateAsync(driver);

        }
    }
}