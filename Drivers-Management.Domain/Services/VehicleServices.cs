using System.Text.RegularExpressions;
using Drivers_Management.Domain.Contracts.Repository;
using Drivers_Management.Domain.Contracts.Services;
using Drivers_Management.Domain.Models;
using Drivers_Management.Domain.Utils;
using FluentValidation;
using OneOf;

namespace Drivers_Management.Domain.Services
{
    // TODO: Implement Base Service to common methods
    public class VehicleServices : IVehicleServices
    {
        private readonly IVehicleRepository _vehicle;
        private readonly IValidator<Vehicle> _validator;
        public VehicleServices(IVehicleRepository vehicle, IValidator<Vehicle> validator)
        {
            _vehicle = vehicle;
            _validator = validator;

        }
        // TODO: 
        public Task<IEnumerable<Vehicle>> GetByAdvancedFilterAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            return await _vehicle.GetAllAsync(pageSize, pageNumber);
        }

        public async Task<Vehicle> GetByPlateAsync(string plate)
        {
            var regex = new Regex("[a-zA-Z]");
            if (!regex.IsMatch(plate))
            {
                throw new DomainExceptions();
            }
            return await _vehicle.GetByPlateAsync(plate);
        }

        public async Task<OneOf<DomainExceptions, int>> PostAsyncllAsync(Vehicle vehicle)
        {
            var validateModel = await _validator.ValidateAsync(vehicle);
            if (!validateModel.IsValid)
                return new DomainExceptions("Sorry, something went wrong. Try again later.");
            vehicle.CreatedAt = DateTime.UtcNow;
            return (await _vehicle.Create(vehicle)).Id;
        }

        public async Task<bool> UpdateAsync(Vehicle vehicle)
        {
            var validateModel = await _validator.ValidateAsync(vehicle);
            var exists = await _vehicle.GetByPlateAsync(vehicle.Plate);
            if (exists is null)
                return false;
            exists.ToUpdate(vehicle);
            return await _vehicle.UpdateAsync(exists);
        }

        public async Task<Vehicle> GetByIdAsync(int id)
        {
            return await _vehicle.GetByIdAsync(id);
        }
    }
}