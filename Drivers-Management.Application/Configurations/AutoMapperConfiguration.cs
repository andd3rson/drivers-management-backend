using AutoMapper;
using Drivers_Management.Application.Controllers;
using Drivers_Management.Application.Dtos;
using Drivers_Management.Domain.Models;

namespace Drivers_Management.Application.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {

            #region Driver
            CreateMap<DriverRequest, Driver>();
            CreateMap<Driver, DriverResponse>();
            CreateMap<Vehicle, DriverVehicleResponse>();
            #endregion

            #region Vehicles 
            CreateMap<VehiclesRequest, Vehicle>();
            CreateMap<Vehicle, VehiclesResponse>();
            CreateMap<Driver, DriversVehicleResponse>();

            #endregion

            CreateMap<UserRegisterRequest, User>()
                .ForMember(dest => dest.PasswordHash, o => o.MapFrom(x => x.Password));
        }
    }
}