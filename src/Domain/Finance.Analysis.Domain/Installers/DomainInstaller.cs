using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Analysis.Domain.Installers;

public static class DomainInstaller
{
    public static void InstallDomain(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.InstallSettings(configuration);
        serviceCollection.InstallMassTransit(configuration);
        serviceCollection.InstallRepositories();
    }
}