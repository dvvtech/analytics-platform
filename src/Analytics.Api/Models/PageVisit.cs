namespace Analytics.Api.Models
{
    public class PageVisit
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
    }

    public class VisitRequest
    {
        /// <summary>
        /// URL страницы, которую посещает пользователь
        /// </summary>
        public string PageUrl { get; set; }

        /// <summary>
        /// URL страницы, с которой пользователь перешел на текущую страницу
        /// </summary>
        public string Referrer { get; set; }
    }
}
