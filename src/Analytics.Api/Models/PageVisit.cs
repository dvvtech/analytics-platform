namespace Analytics.Api.Models
{
    public class PageVisit
    {
        public long Id { get; set; }
        public string SessionId { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string OperatingSystem { get; set; }
        public string Browser { get; set; }
        public string DeviceType { get; set; }
        public string Referrer { get; set; }
        public string PageUrl { get; set; }
        public DateTime VisitTime { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class VisitRequest
    {
        public string PageUrl { get; set; }
        public string Referrer { get; set; }
    }
}
