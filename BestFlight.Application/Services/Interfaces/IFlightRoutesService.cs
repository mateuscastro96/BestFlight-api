using BestFlight.Application.DTOs;
using BestFlight.Infrastructure.Shared;

namespace BestFlight.Application.Services.Interfaces
{
    public interface IFlightRoutesService
    {
        Task<Response<FlightRoutesDto>> FindAsync(Guid id);
        Task<Response<IEnumerable<FlightRoutesDto>>> FindAllAsync();
        Task<Response<FlightRoutesDto>> AddAsync(FlightRoutesDto entity);
        Task<Response<FlightRoutesDto>> UpdateAsync(FlightRoutesDto entity);
        Task<Response<object>> DeleteAsync(Guid id);
        Task<Response<BestFlightRoutesDto>> BestFlightRoutes(string from, string to);
    }
}
