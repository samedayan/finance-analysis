using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Commands.Partners.Responses;
using Finance.Analysis.Contracts.Commands.Partners.Update;
using Finance.Analysis.Domain.Repositories.Partner;

namespace Finance.Analysis.Application.Cqrs.Commands.Partners.Update;

public class PartnerUpdateCommandHandler(IPartnerRepository partnerRepository) : IRequestHandlerWrapper<PartnerUpdateCommand, PartnerResponse>
{
    public async Task<PartnerResponse> Handle(PartnerUpdateCommand request, CancellationToken cancellationToken)
    {
        return await partnerRepository.Update(request, cancellationToken);
    }
}