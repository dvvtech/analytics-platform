using Analytics.Api.BLL.Abstract;

namespace Analytics.Api.BLL.Services
{
    public class IpApiGeoLocationService : IGeoLocationService
    {
        private readonly HttpClient _httpClient;

        public IpApiGeoLocationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://ip-api.com/json/");
        }

        public async Task<(string CountryCode, string CountryName)> GetCountryFromIp(string ipAddress)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IpApiResponse>(ipAddress);
                return (response.CountryCode, response.Country);
            }
            catch
            {
                return ("UN", "Unknown");
            }
        }

        private class IpApiResponse
        {
            public string Country { get; set; }
            public string CountryCode { get; set; }
        }
    }
}
