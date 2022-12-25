using Drivers_Management.Domain.Contracts.Services;
using Drivers_Management.Domain.Models;
using FluentAssertions;
using NSubstitute;
namespace Drivers_Management.Tests.Units.Domain.Services
{
    public class DriverServicesTest
    {
        private readonly IDriverServices _driverServices;
        public DriverServicesTest()
        {
            _driverServices = Substitute.For<IDriverServices>();
        }
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task CreateAsync_ShouldReturnsTrueOrFalse_WhenADriverIsCreate(bool returns)
        {
            // Given
            var driver = new Driver();
            _driverServices.CreateAsync(driver).Returns((driver, returns));
            // When
            var sut = await _driverServices.CreateAsync(driver);
            // Then
            sut.Item2.Should().Be(returns);
        }
    }
}