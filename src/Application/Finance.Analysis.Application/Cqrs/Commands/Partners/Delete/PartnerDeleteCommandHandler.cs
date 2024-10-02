using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Commands.Partners.Delete;
using Finance.Analysis.Contracts.Commands.Partners.Responses;
using Finance.Analysis.Domain.Repositories.Partner;

namespace Finance.Analysis.Application.Cqrs.Commands.Partners.Delete;

public class PartnerDeleteCommandHandler(IPartnerRepository partnerRepository) : IRequestHandlerWrapper<PartnerDeleteCommand, PartnerResponse>
{
    public async Task<PartnerResponse> Handle(PartnerDeleteCommand request, CancellationToken cancellationToken)
    {
        return await partnerRepository.Delete(new Guid(request.PartnerId), cancellationToken);
    }
}