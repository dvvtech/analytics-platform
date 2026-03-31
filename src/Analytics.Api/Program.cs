using Analytics.Api.AppStart;
using Analytics.Api.AppStart.Extensions;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder);
startup.Initialize();

var app = builder.Build();

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
app.Logger.LogInformation(environment ?? "Empty environment");
// Configure the HTTP request pipeline.

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.ApplyCors();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
