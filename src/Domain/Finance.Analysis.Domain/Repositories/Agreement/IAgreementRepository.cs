using Finance.Analysis.Contracts.Commands.Agreements.Add;
using Finance.Analysis.Contracts.Commands.Agreements.Responses;
using Finance.Analysis.Contracts.Commands.Agreements.Update;
using Finance.Analysis.Contracts.Queries.Agreement.Responses;
using Finance.Analysis.Contracts.Queries.Agreement.Search;

namespace Finance.Analysis.Domain.Repositories.Agreement;

public interface IAgreementRepository
{
    public Task<FindAgreementResponse> FindAgreements(FindAgreementQuery request);
    public Task<AgreementResponse> Save(AgreementAddCommand request, CancellationToken cancellationToken);
    public Task<AgreementResponse> Update(AgreementUpdateCommand request, CancellationToken cancellationToken);
    public Task<AgreementResponse> Delete(Guid agreementId, CancellationToken cancellationToken);

}