using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Queries.RiskAnalysis.Responses;

namespace Finance.Analysis.Contracts.Queries.RiskAnalysis.Search;

public class FindRiskAnalysisQuery : IRequestWrapper<FindRiskAnalysisResponse>
{
    public int? Page { get; set; } = 1;
    public int? PageSize { get; set; } = 10;
    public string RiskAnalysisId { get; set; }
}