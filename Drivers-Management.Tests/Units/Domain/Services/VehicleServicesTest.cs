using Drivers_Management.Domain.Contracts.Repository;
using Drivers_Management.Domain.Models;
using Drivers_Management.Domain.Services;
using Drivers_Management.Tests.Fakers;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

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

        [Fact]
        public async Task GetAllAsync_ShouldReturnsEmpty_WhenHasNoItemOnDataBase()
        {
            //Given
            var sut = new VehicleServices(_vehicleRepository, _validator);
            //When
            var response = await sut.GetAllAsync(1, 10);
            //Then
            response.Count().Should().Be(0);

        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnsAList_WhenHasItemOnDataBase()
        {
            //Given
            var takes = 2;
            _vehicleRepository.GetAllAsync(takes, Arg.Any<int>())
                            .Returns(VehiclesFakers.GetVehiclesList().Take(takes));
            var sut = new VehicleServices(_vehicleRepository, _validator);
            //When
            var response = await sut.GetAllAsync(1, takes);
            //Then
            response.Count().Should().Be(takes);
        }


        [Fact]
        public async Task CreateAsync_ShouldReturnsTrue_WhenHasSuccess()

        {
            // Given
            var vehicle = VehiclesFakers.GetVehiclesList().First();
            _vehicleRepository.Create(vehicle).Returns(
                   new Vehicle()
                   {
                       Id = 1,
                       Brand = "BMW",
                       CreatedAt = DateTime.Now
                   }
           );

            _validator.ValidateAsync(Arg.Any<Vehicle>())
                      .Returns(new ValidationResult());
            var sut = new VehicleServices(_vehicleRepository, _validator);
            // When
            var response = await sut.CreateAsync(vehicle);
            // Then
            response.Item2.Should().BeTrue();
            response.Item1.Should().BeOfType<Vehicle>();
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnsFalse_WhenHasErrorAtRepository()
        {
            // Given
            var vehicle = VehiclesFakers.GetVehiclesList().First();
            _vehicleRepository.Create(vehicle).Returns(
                    new Vehicle()

            );

            _validator.ValidateAsync(Arg.Any<Vehicle>()).Returns(new ValidationResult());


            var sut = new VehicleServices(_vehicleRepository, _validator);
            // When
            var response = await sut.CreateAsync(vehicle);
            // Then
            response.Item2.Should().BeFalse();
            response.Item1.Should().BeOfType<Vehicle>();
        }


        [Fact]
        public async Task CreateAsync_ShouldReturnsFalse_WhenInvalidModel()
        {
            // Given
            _validator.ValidateAsync(Arg.Any<Vehicle>()).Returns(new ValidationResult()
            {
                Errors = new List<ValidationFailure>()
                {
                    new ValidationFailure("mock", "something went wrong")
                }
            });

            var sut = new VehicleServices(_vehicleRepository, _validator);
            // When
            var response = await sut.CreateAsync(VehiclesFakers.GetVehiclesList().First());

            // Then
            response.Item2.Should().BeFalse();
            response.Item1.Should().BeOfType<Vehicle>();
        }


        [Fact]
        public async Task UpdateAsync_ShouldReturnFalse_WhenInvalidModel()
        {
            // Given
            _validator.ValidateAsync(Arg.Any<Vehicle>()).Returns(new ValidationResult()
            {
                Errors = new List<ValidationFailure>()
                {
                    new ValidationFailure("Plate", "something went wrong")
                }
            });
            var sut = new VehicleServices(_vehicleRepository, _validator);
            // When
            var result = await sut.UpdateAsync(VehiclesFakers.GetVehiclesList().First());

            // Then
            result.Should().BeFalse();

        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFalse_WhenHasNoDriverToUpdate()
        {
            // Given
            _vehicleRepository.GetByPlateAsync(Arg.Any<string>())
                    .ReturnsNull();
            _validator.ValidateAsync(Arg.Any<Vehicle>())
                      .Returns(new ValidationResult());

            var sut = new VehicleServices(_vehicleRepository, _validator);

            // When
            var result = await sut.UpdateAsync(VehiclesFakers.GetVehiclesList().First());

            // Then
            result.Should().BeFalse();
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnTrue_WhenHasSuccessUpdate()
        {
            // Given
               _validator.ValidateAsync(Arg.Any<Vehicle>())
                      .Returns(new ValidationResult());

             _vehicleRepository.GetByPlateAsync(Arg.Any<string>())
                    .Returns(VehiclesFakers.GetVehiclesList().First());

            _vehicleRepository.UpdateAsync(Arg.Any<Vehicle>()).Returns(true);
           
            var sut = new VehicleServices(_vehicleRepository, _validator);

            // When
            var result = await sut.UpdateAsync(new Vehicle());

            // Then
            result.Should().BeTrue();

        }



    }
}