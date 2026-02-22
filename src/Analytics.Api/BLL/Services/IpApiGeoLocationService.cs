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

        public async Task<LocationInfo> GetLocationFromIp(string ipAddress)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<LocationInfo>($"{ipAddress}?fields=country,countryCode,city");
                return response;
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> GetCountryFromIp(string ipAddress)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<LocationInfo>($"{ipAddress}?fields=country,countryCode");
                return response.Country;
            }
            catch
            {
                return "Unknown";
            }
        }

        public class LocationInfo
        {
            public string Country { get; set; }
            public string CountryCode { get; set; }

            public string City { get; set; }


        }
    }
}
