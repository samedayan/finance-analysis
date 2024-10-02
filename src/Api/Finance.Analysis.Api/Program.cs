
using Finance.Analysis.Api.Extensions;
using Finance.Analysis.Api.HealthChecks;
using Finance.Analysis.Api.Installers;
using Finance.Analysis.Api.Middlewares;
using Finance.Analysis.Application;
using Finance.Analysis.Application.Installers;
using Finance.Analysis.Persistence.PostgresSql;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

Console.WriteLine("Finance Risk Analysis Api Starting...");

var builder = WebApplication.CreateBuilder(args);

ConfigureHostSettings(builder.Host);

Console.WriteLine("Configured Host Settings...");

ConfigurationSettings(builder.Configuration);

Console.WriteLine("Register Services...");

RegisterServices(builder.Services, builder.Configuration);

Console.WriteLine("Services Registered...");

var app = builder.Build();

ConfigureWebApplication(app);

app.Run();


void ConfigureHostSettings(IHostBuilder hostBuilder)
{
    // Wait 30 seconds for graceful shutdown.
    hostBuilder.ConfigureHostOptions(o => o.ShutdownTimeout = TimeSpan.FromSeconds(45));
}

void ConfigurationSettings(IConfigurationBuilder configurationBuilder)
{
    configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
    configurationBuilder.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true);
    configurationBuilder.AddEnvironmentVariables();
}

void RegisterServices(IServiceCollection serviceCollection, IConfiguration configurationRoot)
{
    serviceCollection.InstallSettings(configurationRoot);

    serviceCollection.InstallLoggers(configurationRoot);

    serviceCollection.AddHealthChecks().AddCheck<ReadinessHealthCheck>("readiness-check", HealthStatus.Unhealthy, ["readiness"]);
    
    serviceCollection.AddControllers();
    
    serviceCollection.InstallMassTransit(configurationRoot);
    
    serviceCollection.InstallApplication(configurationRoot);
    
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    serviceCollection.AddEndpointsApiExplorer();
    serviceCollection.AddSwaggerGen();
    serviceCollection.AddRouting(options => options.ConstraintMap.Add("apiVersion", typeof(ProvaRouteConstraint)));
    serviceCollection.AddTransient<CorrelationIdMiddleware>();
    serviceCollection.AddTransient<RequestResponseLoggingMiddleware>();
    //serviceCollection.AddCors();
    
}

void ConfigureWebApplication(IApplicationBuilder applicationBuilder)
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    app.UseCors(
        options => options.WithOrigins("http://localhost:4200")
            
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .SetIsOriginAllowed((host) => true)
            .AllowAnyHeader()
    );

    app.UseCorrelationId();
    app.UseRequestResponseLogger();
    app.UseGlobalExceptionHandler();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllers();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHealthChecks("/health", new HealthCheckOptions()
        {
            Predicate = (check) => !check.Tags.Contains("readiness"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
    });

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHealthChecks("/ready", new HealthCheckOptions()
        {
            Predicate = (check) => check.Tags.Contains("readiness"),
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
    });
}


public class ProvaRouteConstraint : IRouteConstraint
{
    public bool Match(
        HttpContext? httpContext, IRouter? route, string routeKey,
        RouteValueDictionary values, RouteDirection routeDirection)
    {
        return false;
    }
}