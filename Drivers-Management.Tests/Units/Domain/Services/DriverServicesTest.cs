using Drivers_Management.Domain.Contracts.Repository;
using Drivers_Management.Domain.Models;
using Drivers_Management.Domain.Services;
using Drivers_Management.Tests.Fakers;
using FluentAssertions;
using FluentValidation;
using NSubstitute;

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

    }
}