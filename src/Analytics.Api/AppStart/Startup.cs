using Analytics.Api.BLL.Abstract;
using Analytics.Api.BLL.Services;

namespace Analytics.Api.AppStart
{
    public class Startup
    {
        private WebApplicationBuilder _builder;

        public Startup(WebApplicationBuilder builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        public void Initialize()
        {
            if (_builder.Environment.IsDevelopment())
            {
                _builder.Services.AddSwaggerGen();
            }

            //InitConfigs();
            RegisterServices();
            
            _builder.Services.AddControllers();
        }

        private void RegisterServices()
        {
            _builder.Services.AddHttpClient("IpApi", client =>
            {
                client.BaseAddress = new Uri("http://ip-api.com/json/");
            });

            _builder.Services.AddScoped<IGeoLocationService>(serviceProvider =>
            {
                var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient("IpApi");
                return new IpApiGeoLocationService(httpClient);
            });

        }
    }
}
