namespace Analytics.Api.DAL.Entities
{
    public class MppTestsEntity
    {
        public long Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string OperatingSystem { get; set; }
        public string Browser { get; set; }
        public string DeviceType { get; set; }
        public string Operation { get; set; }
        public DateTime VisitTimeUTC { get; set; }
    }
}
