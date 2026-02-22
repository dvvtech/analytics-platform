using static Analytics.Api.BLL.Services.IpApiGeoLocationService;

namespace Analytics.Api.BLL.Abstract
{
    public interface IGeoLocationService
    {
        Task<LocationInfo> GetLocationFromIp(string ipAddress);
        Task<string> GetCountryFromIp(string ipAddress);
    }
}
