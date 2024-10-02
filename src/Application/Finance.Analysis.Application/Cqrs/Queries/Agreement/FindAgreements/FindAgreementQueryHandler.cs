using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Queries.Agreement.Responses;
using Finance.Analysis.Contracts.Queries.Agreement.Search;
using Finance.Analysis.Domain.Repositories.Agreement;

namespace Finance.Analysis.Application.Cqrs.Queries.Agreement.FindAgreements;

public class FindAgreementQueryHandler(IAgreementRepository agreementRepository) : IRequestHandlerWrapper<FindAgreementQuery, FindAgreementResponse>
{
    public async Task<FindAgreementResponse> Handle(FindAgreementQuery request, CancellationToken cancellationToken)
    {
       return await agreementRepository.FindAgreements(request);
    }
}