using Finance.Analysis.Contracts.Commands.RiskAnalysis.Responses;
using Finance.Analysis.Domain.Repositories.RiskAnalysis;

namespace Finance.Analysis.Application.Cqrs.Commands.RiskAnalysis.Delete;

public class RiskAnalysisDeleteCommandHandler(IRiskAnalysisRepository riskAnalysisRepository)
{
    public async Task<RiskAnalysisResponse> Handle(Guid riskAnalysisId, CancellationToken cancellationToken)
    {
        return await riskAnalysisRepository.Delete(riskAnalysisId, cancellationToken);
    }
}