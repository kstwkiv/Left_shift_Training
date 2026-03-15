namespace FlightSearchEngine.Models
{
    public class FlightResult
    {
        public int FlightId { get; set; }
        public string FlightName { get; set; } = string.Empty;
        public string FlightType { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public decimal TotalCost { get; set; }
    }
}
