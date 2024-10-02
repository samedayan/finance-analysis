using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Commands.RiskAnalysis.Add;
using Finance.Analysis.Contracts.Commands.RiskAnalysis.Responses;
using Finance.Analysis.Domain.Repositories.RiskAnalysis;

namespace Finance.Analysis.Application.Cqrs.Commands.RiskAnalysis.Add;

public class RiskAnalysisCommandHandler(IRiskAnalysisRepository riskAnalysisRepository) : IRequestHandlerWrapper<RiskAnalysisAddCommand, RiskAnalysisResponse>
{
    public async Task<RiskAnalysisResponse> Handle(RiskAnalysisAddCommand request, CancellationToken cancellationToken)
    {
        return await riskAnalysisRepository.Save(request, cancellationToken);
    }
}