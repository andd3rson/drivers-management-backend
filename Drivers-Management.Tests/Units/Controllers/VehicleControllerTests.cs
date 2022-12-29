using Drivers_Management.Application.Controllers;
using AutoMapper;
using NSubstitute;
using Drivers_Management.Application.Configurations;
using Drivers_Management.Domain.Contracts.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute.ExceptionExtensions;
using Drivers_Management.Domain.Models;
using Drivers_Management.Application.Dtos;

namespace Drivers_Management.Tests.Units.Controllers
{
    public class VehicleControllerTests
    {
        private readonly IVehicleServices _vehicleService;
        private readonly IMapper _mapper;
        public VehicleControllerTests()
        {
            var config = new MapperConfiguration(x => x.AddProfile<AutoMapperConfiguration>());
            _mapper = config.CreateMapper();
            _vehicleService = Substitute.For<IVehicleServices>();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnsStatusCode200_WhenHasSuccess()
        {
            // Given
            var sut = new VehicleController(_vehicleService, _mapper);

            // When
            var result = await sut.GetAllAsync();

            // Then
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnsAException_WhenThrowsExceptions()
        {
            // Given
            _vehicleService.GetAllAsync(Arg.Any<int>(), Arg.Any<int>()).Throws(new Exception());
            var sut = new VehicleController(_vehicleService, _mapper);

            // When
            try
            {
                await sut.GetAllAsync();

            }
            catch (Exception ex)
            {
                // Then
                ex.Should().BeOfType<Exception>();

            }

        }


        [Fact]
        public async Task GetByPlateAsync_ShouldReturnsStatusCode200_WhenHasSuccess()
        {
            // Given
            string plate = "anaquin";
            _vehicleService.GetByPlateAsync(plate).Returns(new Vehicle()
            {
                Id = 2,
                Plate = plate

            });
            var sut = new VehicleController(_vehicleService, _mapper);
            // When
            var result = await sut.GetByPlateAsync(plate);
            // Then
            result.Should().BeOfType<OkObjectResult>();


        }
        [Fact]
        public async Task PostAsync_ShouldReturnBadRequest_WhenCreatedReturnsFalse()
        {
            // Given
            var sut = new VehicleController(_vehicleService, _mapper);
            // When
            var result = await sut.PostAsync(vehiclesRequest());
            // Then
            result.Should().BeOfType<BadRequestResult>();
        }


        [Fact]
        public async Task PostAsync_ShouldReturnOk_WhenCreatedReturnsTrue()
        {
            // Given
            _vehicleService.CreateAsync(Arg.Any<Vehicle>()).Returns((new Vehicle(), true));
            var sut = new VehicleController(_vehicleService, _mapper);
            // When
            var result = await sut.PostAsync(vehiclesRequest());
            // Then
            result.Should().BeOfType<CreatedResult>();
        }

        private VehiclesRequest vehiclesRequest()
        {
            return new VehiclesRequest("VW", "asraw", "1994");
        }

    }
}