namespace UrlShortner.Entities
{
    public class ApiLog
    {
        public int Id { get; set; }
        public string RequestMethod { get; set; } = string.Empty;
        public string RequestUri { get; set; } = string.Empty;
        public string? RequestContent { get; set; } = string.Empty;

        public string ResponseStatusCode { get; set; } = string.Empty;

        public string? ResponseContent { get; set; } = string.Empty;

        public DateTime LogTime{ get; set; } 

    }
}
