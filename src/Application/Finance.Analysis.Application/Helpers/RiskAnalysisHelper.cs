using Finance.Analysis.Persistence.PostgresSql.Domain.Entities.WorkItems;

namespace Finance.Analysis.Application.Helpers;

public static class RiskAnalysisHelper
{
    public static decimal CalculateRisk(WorkItem workItem)
    {
        var baseRisk = workItem.Name.Contains("Risk") ? 0.7m : 0.3m;
        return baseRisk * 100;
    }
}