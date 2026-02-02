namespace Analytics.Api.BLL.Abstract
{
    public interface IGeoLocationService
    {
        Task<string> GetCountryFromIp(string ipAddress);
    }
}
