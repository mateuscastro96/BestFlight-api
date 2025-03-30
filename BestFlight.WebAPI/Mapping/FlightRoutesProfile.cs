using AutoMapper;
using BestFlight.Application.DTOs;
using BestFlight.Domain.Models;

namespace BestFlight.API.Mapping
{
    public class FlightRoutesProfile : Profile
    {
        public FlightRoutesProfile()
        {
            CreateMap<FlightRoutesDto, FlightRoutes>()
                .ReverseMap();
        }
    }
}