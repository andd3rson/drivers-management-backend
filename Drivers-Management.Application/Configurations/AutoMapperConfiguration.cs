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
        }
    }
}