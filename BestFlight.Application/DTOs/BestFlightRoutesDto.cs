namespace BestFlight.Application.DTOs
{
    public class BestFlightRoutesDto
    {
        public decimal TotalPrice { get; set; } = decimal.MaxValue;
        public List<FlightRoutesDto> Routes { get; set; } = new();
    }
}
