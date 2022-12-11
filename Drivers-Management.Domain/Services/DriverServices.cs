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
        private readonly IValidator<Driver> _validator;
        public DriverServices(IDriverRepository drivers, IValidator<Driver> validator)
        {
            _drivers = drivers;
            _validator = validator;
        }

        public async Task<OneOf<DomainExceptions, Guid>> AddAsync(Driver driver)
        {
            var validateModel = await _validator.ValidateAsync(driver);
            if (!validateModel.IsValid)
                return new DomainExceptions("Sorry, something went wrong. Try again later.");
            driver.CreatedAt = DateTime.UtcNow;
            return (await _drivers.Create(driver)).Id;
        }

        public async Task<IEnumerable<Driver>> GetAllAsync()
        {
            return await _drivers.GetAllAsync();
        }

        public Task<Driver> GetByCpfAsync(int cpf)
        {
            throw new NotImplementedException();
        }


    }
}