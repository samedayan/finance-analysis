using Finance.Analysis.Contracts.Commands.Agreements.Responses;
using Finance.Analysis.Domain.Repositories.Agreement;

namespace Finance.Analysis.Application.Cqrs.Commands.Agreements.Delete;

public class AgreementDeleteCommandHandler(IAgreementRepository agreementRepository)
{
    public async Task<AgreementResponse> Handle(Guid agreementId, CancellationToken cancellationToken)
    {
        return await agreementRepository.Delete(agreementId, cancellationToken);
    }
}