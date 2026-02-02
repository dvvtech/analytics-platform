namespace Analytics.Api.BLL.Abstract
{
    public interface IGeoLocationService
    {
        Task<(string CountryCode, string CountryName)> GetCountryFromIp(string ipAddress);
    }
}
