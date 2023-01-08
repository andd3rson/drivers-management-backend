using System.Text.RegularExpressions;
using Drivers_Management.Domain.Contracts.Repository;
using Drivers_Management.Domain.Contracts.Services;
using Drivers_Management.Domain.Models;
using FluentValidation;

namespace Drivers_Management.Domain.Services
{

    public class VehicleServices : IVehicleServices
    {
        private readonly IVehicleRepository _vehicle;
        private readonly IValidator<Vehicle> _validator;
        public VehicleServices(IVehicleRepository vehicle, IValidator<Vehicle> validator)
        {
            _vehicle = vehicle;
            _validator = validator;
        }

        public async Task<Vehicle> GetByPlateAsync(string plate)
        {
            var regex = new Regex("[a-zA-Z]");
            if (!regex.IsMatch(plate))
            {
                return null;
            }
            return await _vehicle.GetByPlateAsync(plate);
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            return await _vehicle.GetAllAsync(pageSize, pageNumber);
        }

        public async Task<(Vehicle, bool)> CreateAsync(Vehicle vehicle)
        {
            var validateModel = await _validator.ValidateAsync(vehicle);
            if (!validateModel.IsValid)
                return (vehicle, false);
            vehicle.CreatedAt = DateTime.UtcNow;
            var response = await _vehicle.Create(vehicle);
            return (vehicle, response.Id != 0);
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


    }
}