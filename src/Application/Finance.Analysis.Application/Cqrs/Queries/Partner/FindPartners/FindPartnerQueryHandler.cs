using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Queries.Partners.Responses;
using Finance.Analysis.Contracts.Queries.Partners.Search;
using Finance.Analysis.Domain.Repositories.Partner;

namespace Finance.Analysis.Application.Cqrs.Queries.Partner.FindPartners;

public class FindAgreementQueryHandler(IPartnerRepository partnerRepository) : IRequestHandlerWrapper<FindPartnerQuery, FindPartnerResponse>
{
    public async Task<FindPartnerResponse> Handle(FindPartnerQuery request, CancellationToken cancellationToken)
    {
       return await partnerRepository.FindPartners(request);
    }
}