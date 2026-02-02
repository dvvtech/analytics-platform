namespace Analytics.Api.DAL.Entities
{
    public class PageVisitEntity
    {
        public long Id { get; set; }
        public string CountryName { get; set; }
        public string OperatingSystem { get; set; }
        public string Browser { get; set; }
        public string DeviceType { get; set; }
        public string Referrer { get; set; }
        public string PageUrl { get; set; }
        public DateTime VisitTime { get; set; }
        public DateTime CreatedAt { get; set; }

        // Флаг, указывающий, была ли запись агрегирована
        //public bool IsAggregated { get; set; } = false;
    }
}
