using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Commands.Agreements.Add;
using Finance.Analysis.Contracts.Commands.Agreements.Responses;
using Finance.Analysis.Domain.Repositories.Agreement;

namespace Finance.Analysis.Application.Cqrs.Commands.Agreements.Add;

public class AgreementAddCommandHandler(IAgreementRepository agreementRepository) : IRequestHandlerWrapper<AgreementAddCommand, AgreementResponse>
{
    public async Task<AgreementResponse> Handle(AgreementAddCommand request, CancellationToken cancellationToken)
    {
        return await agreementRepository.Save(request, cancellationToken);
    }
}