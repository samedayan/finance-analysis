using Finance.Analysis.Contracts.Commands.RiskAnalysis.Add;
using Finance.Analysis.Contracts.Commands.RiskAnalysis.Responses;
using Finance.Analysis.Contracts.Commands.RiskAnalysis.Update;
using Finance.Analysis.Contracts.Queries.RiskAnalysis.Responses;
using Finance.Analysis.Contracts.Queries.RiskAnalysis.Search;

namespace Finance.Analysis.Domain.Repositories.RiskAnalysis;

public interface IRiskAnalysisRepository
{
    public Task<FindRiskAnalysisResponse> FindRiskAnalysis(FindRiskAnalysisQuery request);
    public Task<RiskAnalysisResponse> Save(RiskAnalysisAddCommand request, CancellationToken cancellationToken);
    public Task<RiskAnalysisResponse> Update(RiskAnalysisUpdateCommand request, CancellationToken cancellationToken);
    public Task<RiskAnalysisResponse> Delete(Guid partnerId, CancellationToken cancellationToken);
}