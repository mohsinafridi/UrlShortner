namespace UrlShortner.Models
{ 
    public class ActivityModel
    {
        public string Activity { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Participants { get; set; }
        public decimal Price { get; set; }
        public string Link { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public double Accessibility { get; set; }
    }
}
