using AutoMapper;
using Drivers_Management.Application.Dtos;
using Drivers_Management.Domain.Models;

namespace Drivers_Management.Application.Configurations
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<VehiclesRequest, Vehicle>().ReverseMap();
            CreateMap<Vehicle, VehiclesResponse>()
            .ReverseMap();

            // Vehicle response not mapped.

            #region configuration map to driver
            CreateMap<DriverRequest, Driver>()
                .ReverseMap();


            CreateMap<Driver, DriverResponse>()
            .ForMember(dest => dest.Vehicle, opt =>
                    opt.MapFrom(x => x.Vehicles.Select(x => x.Vehicles)));

            #endregion

            CreateMap<DriverVehicle, VehiclesResponse>()
                .ForMember(d => d.Id, o => o.MapFrom(x => x.Vehicles.Id))
                .ForMember(d => d.Plate, o => o.MapFrom(x => x.Vehicles.Plate))
                .ForMember(d => d.Year, o => o.MapFrom(x => x.Vehicles.Year))
                .ForMember(d => d.Brand, o => o.MapFrom(x => x.Vehicles.Brand));

            CreateMap<DriverVehicle, DriverResponse>()
                .ForMember(d => d.Vehicle, o => o.MapFrom(x => x.Vehicles));


            CreateMap<Driver, DriversResponse>();

            CreateMap<DriverVehicle, DriversResponse>()
                .ForMember(d => d.Cpf, o => o.MapFrom(x => x.Drivers.Id))
                .ForMember(d => d.Cpf, o => o.MapFrom(x => x.Drivers.Name))
                .ForMember(d => d.Cpf, o => o.MapFrom(x => x.Drivers.Email))
                .ForMember(d => d.Cpf, o => o.MapFrom(x => x.Drivers.Cpf));



        }
    }
}