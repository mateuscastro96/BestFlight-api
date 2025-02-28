using BestFlight.Application.DTOs;
using BestFlight.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BestFlight.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightRoutesController(IFlightRoutesService service) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(Guid id)
        {
            var routes = await service.FindAsync(id);
            if (routes.Data == null)
                return NotFound();
            return Ok(routes);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var routes = await service.FindAllAsync();
            return Ok(routes);
        }

        [HttpPost]
        public async Task<IActionResult> Add(FlightRoutesDto flightRoutesDto)
        {
            var result = await service.AddAsync(flightRoutesDto);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(FlightRoutesDto flightRoutesDto)
        {
            var result = service.UpdateAsync(flightRoutesDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var result = service.DeleteAsync(id);
            return Ok(result);
        }

        [HttpGet("/good-routes/{from}/{to}")]
        public async Task<IActionResult> GoodRoutes(string from, string to)
        {
            return Ok(await service.BestFlightRoutes(from, to));
        }
    }
}
