using Finance.Analysis.Domain.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Finance.Analysis.Application.Installers;

public static class ApplicationInstaller
{
    public static void InstallApplication(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.InstallDomain(configuration);
        serviceCollection.InstallMediatr();
    }
}