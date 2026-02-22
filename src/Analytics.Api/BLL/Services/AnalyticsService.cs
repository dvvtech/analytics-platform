
using Analytics.Api.BLL.Abstract;
using Analytics.Api.DAL;
using Analytics.Api.DAL.Entities;

namespace Analytics.Api.BLL.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly AnalyticsDbContext _dbContext;
        private readonly IGeoLocationService _geoService;
        private readonly ILogger<AnalyticsService> _logger;

        public AnalyticsService(
            AnalyticsDbContext analyticsDbContext,
            IGeoLocationService geoService,
            ILogger<AnalyticsService> logger)
        {
            _dbContext = analyticsDbContext;
            _geoService = geoService;
            _logger = logger;
        }

        public async Task TrackVisitOfftubeTechAsync(string ipAddress, string userAgent, string referrer, string pageUrl)
        {
            try
            {
                var visit = new OfftubeTechEntity();
                //{                    
                //    VisitTimeUTC = DateTime.UtcNow,                 
                //};

                var (os, browser, device) = UserAgentParser.Parse(userAgent);
                visit.OperatingSystem = os;
                visit.Browser = browser;
                visit.DeviceType = device;

                if (!string.IsNullOrEmpty(ipAddress) && ipAddress != "::1" && ipAddress != ":" && ipAddress != "localhost")
                {
                    try
                    {
                        var location = await _geoService.GetLocationFromIp(ipAddress);
                        visit.Country = location.Country;
                        visit.City = location.City;
                    }
                    catch (Exception)
                    {
                        _logger.LogError("Fail get country name");
                    }
                }

                _dbContext.OfftubeTech.Add(visit);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("TrackVisit error", ex);
            }
        }

        public async Task TrackVisitAsync(string ipAddress, string userAgent, string referrer, string pageUrl)
        {
            try
            {
                var visit = new PageVisitEntity
                {
                    Referrer = referrer,
                    PageUrl = pageUrl,
                    VisitTime = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow //todo удалить поле
                };

                var (os, browser, device) = UserAgentParser.Parse(userAgent);
                visit.OperatingSystem = os;
                visit.Browser = browser;
                visit.DeviceType = device;

                if (!string.IsNullOrEmpty(ipAddress) && ipAddress != "::1" && ipAddress != ":" && ipAddress != "localhost")
                {
                    try
                    {
                        var countryName = await _geoService.GetCountryFromIp(ipAddress);
                        visit.CountryName = countryName;
                    }
                    catch (Exception)
                    {
                        _logger.LogError("Fail get country name");
                    }
                }

                _dbContext.PageVisits.Add(visit);
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError("TrackVisit error", ex);
            }
        }
    }
}
