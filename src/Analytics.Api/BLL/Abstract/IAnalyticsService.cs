namespace Analytics.Api.BLL.Abstract
{
    public interface IAnalyticsService
    {
        Task TrackVisitOfftubeTechAsync(string ipAddress, string userAgent, string mediaUrl);

        Task TrackVisitAsync(string ipAddress, string userAgent, string referrer, string pageUrl);
    }
}
