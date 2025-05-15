namespace Geolocalizacion.Models
{
    public class RangeExit
    {
        public int exitId { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string cardId { get; set; } = string.Empty;

    }
    }
