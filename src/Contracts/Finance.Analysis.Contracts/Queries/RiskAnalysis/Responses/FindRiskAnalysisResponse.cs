using Finance.Analysis.Contracts.Queries.RiskAnalysis.ViewModels;

namespace Finance.Analysis.Contracts.Queries.RiskAnalysis.Responses;

public class FindRiskAnalysisResponse
{
    public List<FindRiskAnalysisViewModel> Data { get; set; } = [];
    public int TotalCount { get; set; }
}