using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Commands.RiskAnalysis.Responses;
using Finance.Analysis.Contracts.Commands.RiskAnalysis.Update;
using Finance.Analysis.Domain.Repositories.RiskAnalysis;

namespace Finance.Analysis.Application.Cqrs.Commands.RiskAnalysis.Update;

public class RiskAnalysisUpdateCommandHandler(IRiskAnalysisRepository riskAnalysisRepository) : IRequestHandlerWrapper<RiskAnalysisUpdateCommand, RiskAnalysisResponse>
{
    public async Task<RiskAnalysisResponse> Handle(RiskAnalysisUpdateCommand request, CancellationToken cancellationToken)
    {
        return await riskAnalysisRepository.Update(request, cancellationToken);
    }
}