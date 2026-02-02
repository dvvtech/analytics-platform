using Analytics.Api.BLL.Abstract;

namespace Analytics.Api.BLL.Services
{
    public class IpApiGeoLocationService : IGeoLocationService
    {
        private readonly HttpClient _httpClient;

        public IpApiGeoLocationService(HttpClient httpClient)
        {            
            _httpClient = httpClient;            
        }

        public async Task<string> GetCountryFromIp(string ipAddress)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<IpApiResponse>(ipAddress);
                return response.Country;
            }
            catch
            {
                return "Unknown";
            }
        }

        private class IpApiResponse
        {
            public string Country { get; set; }
            public string CountryCode { get; set; }
        }
    }
}
