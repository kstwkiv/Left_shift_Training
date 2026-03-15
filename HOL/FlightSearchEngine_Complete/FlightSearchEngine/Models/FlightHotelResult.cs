namespace FlightSearchEngine.Models
{
    public class FlightHotelResult
    {
        public int FlightId { get; set; }
        public string FlightName { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public string HotelName { get; set; } = string.Empty;
        public decimal TotalCost { get; set; }
    }
}
