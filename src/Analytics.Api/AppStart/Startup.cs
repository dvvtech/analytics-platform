using Analytics.Api.BLL.Abstract;
using Analytics.Api.BLL.Services;
using Analytics.Api.Configurations;
using Analytics.Api.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
            SetupDb();

            _builder.Services.AddControllers();
        }

        private void InitConfigs()
        {
            _builder.Services.Configure<DatabaseOptions>(_builder.Configuration.GetSection(DatabaseOptions.SectionName));

            //_builder.Services
            //    .AddOptions<DatabaseOptions>()
            //    .BindConfiguration(nameof(DatabaseOptions))
            //    .ValidateDataAnnotations()
            //    .ValidateOnStart();
        }

        private void SetupDb()
        {
            _builder.Services.AddDAL(_builder.Configuration);
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
