using Finance.Analysis.Domain.Settings.MassTransit;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Analysis.Domain.Installers;

public static class MassTransitInstaller
{
    public static void InstallMassTransit(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var busSettings = new BusSettings();
        configuration.GetSection("BusSettings").Bind(busSettings);
        //MassTransit
        // useful documentation https://masstransit-project.com/
        // lots of great example and scenarios https://www.youtube.com/user/PhatBoyG
        serviceCollection.AddMassTransit(x =>
        {
            // init bus
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri($"{busSettings.ClusterAddress}"), h =>
                {
                    h.Username(busSettings.UserName);
                    h.Password(busSettings.Password);
                });
            });
        });
    }
}