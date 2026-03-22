namespace Analytics.Api.BLL.Abstract
{
    public interface IAnalyticsService
    {
        Task TrackVisitLinkSummaryAsync(string ipAddress, string userAgent, string link);

        Task TrackVisitMppTestAsync(string ipAddress, string userAgent, string operation);

        Task TrackVisitOfftubeTechAsync(string ipAddress, string userAgent, string mediaUrl);

        Task TrackVisitAsync(string ipAddress, string userAgent, string referrer, string pageUrl);
    }
}
