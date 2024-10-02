using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Commands.Agreements.Responses;
using Finance.Analysis.Contracts.Commands.Agreements.Update;
using Finance.Analysis.Domain.Repositories.Agreement;

namespace Finance.Analysis.Application.Cqrs.Commands.Agreements.Update;

public class AgreementUpdateCommandHandler(IAgreementRepository agreementRepository) : IRequestHandlerWrapper<AgreementUpdateCommand, AgreementResponse>
{
    public async Task<AgreementResponse> Handle(AgreementUpdateCommand request, CancellationToken cancellationToken)
    {
        return await agreementRepository.Update(request, cancellationToken);
    }
}