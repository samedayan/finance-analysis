using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Commands.Partners.Add;
using Finance.Analysis.Contracts.Commands.Partners.Responses;
using Finance.Analysis.Domain.Repositories.Partner;

namespace Finance.Analysis.Application.Cqrs.Commands.Partners.Add;

public class PartnerAddCommandHandler(IPartnerRepository partnerRepository) : IRequestHandlerWrapper<PartnerAddCommand, PartnerResponse>
{
    public async Task<PartnerResponse> Handle(PartnerAddCommand request, CancellationToken cancellationToken)
    {
        return await partnerRepository.Save(request, cancellationToken);
    }
}