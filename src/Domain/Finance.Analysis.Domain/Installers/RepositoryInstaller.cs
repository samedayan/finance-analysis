using Finance.Analysis.Domain.Repositories.Agreement;
using Finance.Analysis.Domain.Repositories.Partner;
using Finance.Analysis.Domain.Repositories.RiskAnalysis;
using Finance.Analysis.Domain.Repositories.WorkItem;
using Finance.Analysis.Domain.Settings.PostgresSql;
using Finance.Analysis.Persistence.PostgresSql.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Finance.Analysis.Domain.Installers;

public static class RepositoryInstaller
{
    public static void InstallRepositories(this IServiceCollection serviceCollection)
    {
        var postgresSettings = new PostgresSettings();
        var configuration = serviceCollection.BuildServiceProvider().GetRequiredService<IConfiguration>();
        configuration.GetSection(nameof(PostgresSettings)).Bind(postgresSettings);

        // https://docs.microsoft.com/en-us/ef/core/performance/advanced-performance-topics?tabs=with-di%2Cwith-constant#dbcontext-pooling
        serviceCollection.AddPooledDbContextFactory<FinanceRiskAnalysisContext>(opt =>
        {
            opt.UseNpgsql(postgresSettings.ConnectionString, builder =>
            {
                builder.CommandTimeout(60000);
                builder.EnableRetryOnFailure(3);
            }).EnableSensitiveDataLogging();
            opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            opt.LogTo(Console.WriteLine, LogLevel.Information);
        });
        
        serviceCollection.AddScoped<IAgreementRepository, AgreementRepository>();
        serviceCollection.AddScoped<IWorkItemRepository, WorkItemRepository>();
        serviceCollection.AddScoped<IPartnerRepository, PartnerRepository>();
        serviceCollection.AddScoped<IRiskAnalysisRepository, RiskAnalysisRepository>();
    }
}