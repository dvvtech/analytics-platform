using Analytics.Api.BLL.Services;

namespace Analytics.Api.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var userAgent = "Mozilla/5.0 (Linux; Android 10; K) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/144.0.0.0 Mobile Safari/537.36\r\n";
            var res = UserAgentParser.Parse(userAgent);
        }

        [Fact]
        public async Task Test2()
        {
            var client = new HttpClient()
            { 
                BaseAddress = new Uri("http://ip-api.com/json/")
            };

            var ipApiGeoLocationService = new IpApiGeoLocationService(client);
            var res = await ipApiGeoLocationService.GetLocationFromIp("94.19.16.117");
        }
    }
}
