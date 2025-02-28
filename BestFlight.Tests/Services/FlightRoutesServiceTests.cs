using AutoMapper;
using BestFlight.Application.DTOs;
using BestFlight.Application.Services;
using BestFlight.Domain.Models;
using BestFlight.Infrastructure.Repositories.Interfaces;
using Moq;

namespace BestFlight.Tests.Services
{
    public class FlightRoutesServiceTests
    {
        private readonly Mock<IFlightRoutesRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly FlightRoutesService _service;

        public FlightRoutesServiceTests()
        {
            _repositoryMock = new Mock<IFlightRoutesRepository>();
            _mapperMock = new Mock<IMapper>();
            _service = new FlightRoutesService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task FindAsync_ShouldReturnFlightRoute_WhenRouteExists()
        {
            var id = Guid.NewGuid();
            var flightRoute = new FlightRoutes { Id = id, From = "A", To = "B", Price = 100 };
            var flightRouteDto = new FlightRoutesDto { Id = id, From = "A", To = "B", Price = 100 };

            _repositoryMock.Setup(repo => repo.FindAsync(id)).ReturnsAsync(flightRoute);
            _mapperMock.Setup(m => m.Map<FlightRoutesDto>(flightRoute)).Returns(flightRouteDto);

            var result = await _service.FindAsync(id);

            Assert.True(result.Success);
            Assert.Equal(id, result.Data.Id);
        }

        [Fact]
        public async Task FindAsync_ShouldReturnFailure_WhenRouteDoesNotExist()
        {
            _repositoryMock.Setup(repo => repo.FindAsync(It.IsAny<Guid>())).ReturnsAsync((FlightRoutes?)null);

            var result = await _service.FindAsync(Guid.NewGuid());

            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task FindAllAsync_ShouldReturnAllFlightRoutes()
        {
            var routes = new List<FlightRoutes> { new FlightRoutes { From = "A", To = "B", Price = 100 } };
            var routesDto = new List<FlightRoutesDto> { new FlightRoutesDto { From = "A", To = "B", Price = 100 } };

            _repositoryMock.Setup(repo => repo.FindAllAsync()).ReturnsAsync(routes);
            _mapperMock.Setup(m => m.Map<IEnumerable<FlightRoutesDto>>(routes)).Returns(routesDto);

            var result = await _service.FindAllAsync();

            Assert.True(result.Success);
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task AddAsync_ShouldAddFlightRoute()
        {
            var dto = new FlightRoutesDto { From = "A", To = "B", Price = 100 };    
            var entity = new FlightRoutes { From = "A", To = "B", Price = 100 };

            _mapperMock.Setup(m => m.Map<FlightRoutes>(dto)).Returns(entity);
            _repositoryMock.Setup(repo => repo.CreateAsync(entity)).ReturnsAsync(entity);

            var result = await _service.AddAsync(dto);

            Assert.True(result.Success);
            Assert.Equal(dto.From, result.Data.From);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateFlightRoute()
        {
            var dto = new FlightRoutesDto { From = "A", To = "B", Price = 150 };
            var entity = new FlightRoutes { From = "A", To = "B", Price = 150 };

            _mapperMock.Setup(m => m.Map<FlightRoutes>(dto)).Returns(entity);
            _repositoryMock.Setup(repo => repo.Update(entity)).ReturnsAsync(entity);

            var result = await _service.UpdateAsync(dto);

            Assert.True(result.Success);
            Assert.Equal(dto.Price, result.Data.Price);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteFlightRoute_WhenRouteExists()
        {
            var id = Guid.NewGuid();
            var flightRoute = new FlightRoutes { Id = id, From = "A", To = "B", Price = 100 };

            _repositoryMock.Setup(repo => repo.FindAsync(id)).ReturnsAsync(flightRoute);
            _repositoryMock.Setup(repo => repo.DeleteAsync(flightRoute)).Returns(Task.CompletedTask);

            var result = await _service.DeleteAsync(id);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFailure_WhenRouteDoesNotExist()
        {
            _repositoryMock.Setup(repo => repo.FindAsync(It.IsAny<Guid>())).ReturnsAsync((FlightRoutes?)null);

            var result = await _service.DeleteAsync(Guid.NewGuid());

            Assert.False(result.Success);
        }
    }
}
