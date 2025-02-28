namespace BestFlight.Application.DTOs
{
    public class FlightRoutesDto
    {
        public Guid Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public decimal Price { get; set; }
    }
}
