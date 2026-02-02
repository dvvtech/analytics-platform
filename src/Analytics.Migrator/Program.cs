using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var environmentName = args.FirstOrDefault() ??
                              Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??
                              throw new InvalidOperationException("ASPNETCORE_ENVIRONMENT in not set");

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.{environmentName}.json")
    .Build();

var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<IConfiguration>(configuration);
//serviceCollection.AddTransient<MigrationService>();