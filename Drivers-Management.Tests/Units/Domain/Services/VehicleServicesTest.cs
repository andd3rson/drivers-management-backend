using Drivers_Management.Domain.Contracts.Repository;
using Drivers_Management.Domain.Models;
using Drivers_Management.Domain.Services;
using FluentAssertions;
using FluentValidation;
using NSubstitute;

namespace Drivers_Management.Tests.Units.Domain.Services
{
    public class VehicleServicesTest
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IValidator<Vehicle> _validator;
        public VehicleServicesTest()
        {
            _vehicleRepository = Substitute.For<IVehicleRepository>();
            _validator = Substitute.For<IValidator<Vehicle>>();
        }


        [Fact]
        public async Task GetByPlateAsync_ShouldReturnsNull_WhenInvalidArgument()
        {
            // Given
            string plate = "1212afaas";
            var sut = new VehicleServices(_vehicleRepository, _validator);

            // When
            var response = await sut.GetByPlateAsync(plate);
            // Then               
            response.Should().BeNull();
        }

        [Fact]
        public async Task GetByPlateAsync_ShouldReturnsValidDate_WhenIsAvalidArgument()
        {
            // Given
            string plate = "RTafaas";
            _vehicleRepository.GetByPlateAsync(plate).Returns(new Vehicle
            {
                Plate = plate
            });
            var sut = new VehicleServices(_vehicleRepository, _validator);

            // When
            var response = await sut.GetByPlateAsync(plate);
            // Then               
            response.Should().NotBeNull();
            response.Plate.Should().Be(plate);
        }
    }
}