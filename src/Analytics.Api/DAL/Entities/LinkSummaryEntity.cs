namespace Analytics.Api.DAL.Entities
{
    public class LinkSummaryEntity
    {
        public long Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string OperatingSystem { get; set; }
        public string Browser { get; set; }
        public string DeviceType { get; set; }
        public string Link { get; set; }
        public DateTime VisitTimeUTC { get; set; }
    }
}
