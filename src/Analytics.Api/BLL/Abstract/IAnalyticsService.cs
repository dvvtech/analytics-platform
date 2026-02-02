namespace Analytics.Api.BLL.Abstract
{
    public interface IAnalyticsService
    {
        Task TrackVisitAsync(string ipAddress, string userAgent, string referrer, string pageUrl);
    }
}
