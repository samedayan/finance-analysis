using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Commands.RiskAnalysis.Responses;

namespace Finance.Analysis.Contracts.Commands.RiskAnalysis.Update;

public class RiskAnalysisUpdateCommand : IRequestWrapper<RiskAnalysisResponse>
{
    public Guid RiskAnalysisId { get; set; }
}