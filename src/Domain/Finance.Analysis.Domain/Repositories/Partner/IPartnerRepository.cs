using Finance.Analysis.Contracts.Commands.Partners.Add;
using Finance.Analysis.Contracts.Commands.Partners.Responses;
using Finance.Analysis.Contracts.Commands.Partners.Update;
using Finance.Analysis.Contracts.Queries.Partners.Responses;
using Finance.Analysis.Contracts.Queries.Partners.Search;

namespace Finance.Analysis.Domain.Repositories.Partner;

public interface IPartnerRepository
{
    public Task<FindPartnerResponse> FindPartners(FindPartnerQuery request);
    public Task<PartnerResponse> Save(PartnerAddCommand request, CancellationToken cancellationToken);
    public Task<PartnerResponse> Update(PartnerUpdateCommand request, CancellationToken cancellationToken);
    public Task<PartnerResponse> Delete(Guid partnerId, CancellationToken cancellationToken);
}