using AutoMapper;
using BestFlight.Application.DTOs;
using BestFlight.Application.Services.Interfaces;
using BestFlight.Domain.Models;
using BestFlight.Infrastructure.Repositories.Interfaces;
using BestFlight.Infrastructure.Shared;

namespace BestFlight.Application.Services
{
    public class FlightRoutesService(IFlightRoutesRepository repository, IMapper mapper) : IFlightRoutesService
    {
        public async Task<Response<FlightRoutesDto>> FindAsync(Guid id) =>
            await ExecuteWithResponse(async () =>
            {
                var result = await repository.FindAsync(id);
                return result is not null ? mapper.Map<FlightRoutesDto>(result) : null;
            });

        public async Task<Response<IEnumerable<FlightRoutesDto>>> FindAllAsync() =>
            await ExecuteWithResponse(async () =>
            {
                var result = await repository.FindAllAsync();
                return mapper.Map<IEnumerable<FlightRoutesDto>>(result);
            });

        public async Task<Response<FlightRoutesDto>> AddAsync(FlightRoutesDto flightRoutesDto) =>
            await ExecuteWithResponse(async () =>
            {
                var entity = mapper.Map<FlightRoutes>(flightRoutesDto);
                await repository.CreateAsync(entity);
                return flightRoutesDto;
            });

        public async Task<Response<FlightRoutesDto>> UpdateAsync(FlightRoutesDto flightRoutesDto) =>
            await ExecuteWithResponse(async () =>
            {
                var entity = mapper.Map<FlightRoutes>(flightRoutesDto);
                await repository.Update(entity);
                return flightRoutesDto;
            });

        public async Task<Response<object>> DeleteAsync(Guid id) =>
            await ExecuteWithResponse(async () =>
            {
                var entity = await repository.FindAsync(id);
                if (entity is null)
                    return null;

                await repository.DeleteAsync(entity);
                return new object();
            });


        public async Task<Response<BestFlightRoutesDto>> BestFlightRoutes(string from, string to)
        {
            var allRoutes = await repository.FindAllAsync();
            if (!allRoutes.Any())
                return Response<BestFlightRoutesDto>.Fail("Nenhuma rota localizada");

            var bestRoute = FindCheapestRoute(allRoutes, from, to);
            return bestRoute is not null
                ? Response<BestFlightRoutesDto>.Ok(bestRoute)
                : Response<BestFlightRoutesDto>.Fail("Nenhuma rota encontrada.");
        }

        private BestFlightRoutesDto? FindCheapestRoute(IEnumerable<FlightRoutes> routes, string from, string to)
        {
            var priorityQueue = new PriorityQueue<(List<FlightRoutes> Path, decimal Price), decimal>();
            priorityQueue.Enqueue((new List<FlightRoutes>(), 0), 0);

            var bestPrice = new Dictionary<string, decimal> { { from, 0 } };

            while (priorityQueue.Count > 0)
            {
                var (currentRoute, currentPrice) = priorityQueue.Dequeue();
                var lastAirport = currentRoute.Count > 0 ? currentRoute.Last().To : from;

                if (lastAirport == to)
                {
                    return new BestFlightRoutesDto
                    {
                        TotalPrice = currentPrice,
                        Routes = currentRoute.Select(mapper.Map<FlightRoutesDto>).ToList()
                    };
                }

                foreach (var route in routes.Where(r => r.From == lastAirport))
                {
                    var newPrice = currentPrice + route.Price;

                    if (!bestPrice.ContainsKey(route.To) || newPrice < bestPrice[route.To])
                    {
                        bestPrice[route.To] = newPrice;
                        var newRoute = new List<FlightRoutes>(currentRoute) { route };
                        priorityQueue.Enqueue((newRoute, newPrice), newPrice);
                    }
                }
            }

            return null;
        }

        private static async Task<Response<T>> ExecuteWithResponse<T>(Func<Task<T?>> action)
        {
            try
            {
                var result = await action();
                return result is not null
                    ? Response<T>.Ok(result)
                    : Response<T>.Fail("Nenhum dado encontrado.");
            }
            catch (Exception ex)
            {
                return Response<T>.Fail("Ocorreu um erro ao processar a requisição.", new List<string> { ex.Message });
            }
        }
    }
}
