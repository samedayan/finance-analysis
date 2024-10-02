using Finance.Analysis.Domain.Settings.MassTransit;
using Finance.Analysis.Domain.Settings.PostgresSql;
using Finance.Analysis.Domain.Validations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Finance.Analysis.Domain.Installers;

public static class SettingsInstaller
{
    public static void InstallSettings(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddOptions<PostgresSettings>().ValidateOnStart();

        serviceCollection.Configure<PostgresSettings>(configuration.GetSection(nameof(PostgresSettings)));
        var postgresSettings = configuration.Get<PostgresSettings>();
        configuration.GetSection(nameof(PostgresSettings)).Bind(postgresSettings);
        serviceCollection.AddSingleton(postgresSettings);

        serviceCollection.Configure<BusSettings>(configuration.GetSection(nameof(BusSettings)));
        var busSettings = configuration.Get<BusSettings>();
        configuration.GetSection(nameof(BusSettings)).Bind(busSettings);
        serviceCollection.AddSingleton(busSettings);

        serviceCollection.AddSingleton<IValidateOptions<PostgresSettings>, PostgresSettingsValidation>();
        serviceCollection.AddSingleton<IValidateOptions<BusSettings>, MassTransitSettingsValidation>();
    }
}