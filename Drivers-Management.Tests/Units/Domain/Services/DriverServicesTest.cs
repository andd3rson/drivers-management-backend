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
    public class DriverServicesTest
    {

        private readonly IDriverRepository _driverRepository;
        private readonly IValidator<Driver> _validator;
        public DriverServicesTest()
        {
            _driverRepository = Substitute.For<IDriverRepository>();
            _validator = Substitute.For<IValidator<Driver>>();
        }

        [Fact]
        public async Task GetByCpfAsync_ShouldReturnsNull_WhenInvalidArgument()
        {
            //Given
            string cpf = "";
            _driverRepository.GetByCpfAsync(cpf).ReturnsNull();
            var sut = new DriverServices(_driverRepository, _validator, null);
            //When
            var response = await sut.GetByCpfAsync(cpf);
            //Then
            response.Should().BeNull();

        }

        [Fact]
        public async Task GetByCpfAsync_ShouldReturnsDriver_WhenIsValidArgument()
        {
            //Given
            _driverRepository.GetByCpfAsync(Arg.Any<string>()).Returns(DriversFakers.TakeOneDriver());
            var sut = new DriverServices(_driverRepository, _validator, null);
            //When
            var response = await sut.GetByCpfAsync("84512154086");
            //Then
            response.Should().NotBeNull();

        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnsEmpty_WhenHasNoItemOnDataBase()
        {
            //Given
            var sut = new DriverServices(_driverRepository, _validator, null);
            //When
            var response = await sut.GetAllAsync(1, 10);
            //Then
            response.Count().Should().Be(0);

        }

        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(7)]
        public async Task GetAllAsync_ShouldReturnsAList_WhenHasItemOnDataBase(int takes)
        {
            //Given
            _driverRepository.GetAllAsync(takes, Arg.Any<int>())
                            .Returns(DriversFakers.listDrivers().Take(takes));
            var sut = new DriverServices(_driverRepository, _validator, null);
            //When
            var response = await sut.GetAllAsync(1, takes);
            //Then
            response.Count().Should().Be(takes);
        }


        [Fact]
        public async Task CreateAsync_ShouldReturnsTrue_WhenHasSuccess()

        {
            // Given
            var driver = DriversFakers.TakeOneDriver();
            _driverRepository.Create(driver).Returns(
                    new Driver
                    {
                        Id = 1,
                        Cpf = driver.Cpf,
                        Email = driver.Email,
                        Name = driver.Name
                    }
            );

            _validator.ValidateAsync(driver).Returns(new ValidationResult());
            var sut = new DriverServices(_driverRepository, _validator, null);
            // When
            var response = await sut.CreateAsync(driver);
            // Then
            response.Item2.Should().BeTrue();
            response.Item1.Should().BeOfType<Driver>();
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnsFalse_WhenHasErrorAtRepository()
        {
            // Given
            var driver = DriversFakers.TakeOneDriver();
            _driverRepository.Create(driver).Returns(
                    new Driver()

            );

            _validator.ValidateAsync(driver).Returns(new ValidationResult());


            var sut = new DriverServices(_driverRepository, _validator, null);
            // When
            var response = await sut.CreateAsync(driver);
            // Then
            response.Item2.Should().BeFalse();
            response.Item1.Should().BeOfType<Driver>();
        }


        [Fact]
        public async Task CreateAsync_ShouldReturnsFalse_WhenInvalidModel()
        {
            // Given
            var driver = DriversFakers.TakeOneDriver();
            _driverRepository.Create(driver).Returns(
                    new Driver
                    {
                        Id = 1,
                        Cpf = driver.Cpf,
                        Email = driver.Email,
                        Name = driver.Name
                    }
            );

            _validator.ValidateAsync(driver).Returns(new ValidationResult()
            {
                Errors = new List<ValidationFailure>()
                {
                    new ValidationFailure("mock", "something went wrong")
                }
            });
            var sut = new DriverServices(_driverRepository, _validator, null);
            // When
            var response = await sut.CreateAsync(driver);

            // Then
            response.Item2.Should().BeFalse();
            response.Item1.Should().BeOfType<Driver>();
        }

    }
}