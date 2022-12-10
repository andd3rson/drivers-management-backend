using Drivers_Management.Domain.Contracts.Repository;
using Drivers_Management.Domain.Contracts.Services;
using Drivers_Management.Domain.Models;
using FluentValidation;

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

        // TO DO: Implement OneOf(,). It will be better in case any error ocurrs.
        public async Task<Guid> AddAsync(Driver driver)
        {
            if (!(await _validator.ValidateAsync(driver)).IsValid)
            {
                // TO DO: Add a domain exception class.
                return Guid.Empty;
            }
            driver.CreatedAt = DateTime.UtcNow;
            var result = await _drivers.Create(driver);
            return result.Id;
        }

        public async Task<IEnumerable<Driver>> GetAllAsync()
        {
            return await _drivers.GetAllAsync();
        }

        public  Task<Driver> GetByCpfAsync(int cpf)
        {
            throw new NotImplementedException();
        }

    }
}