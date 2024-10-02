using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Commands.Partners.Responses;

namespace Finance.Analysis.Contracts.Commands.Partners.Update;

public class PartnerUpdateCommand : IRequestWrapper<PartnerResponse>
{
    public Guid PartnerId { get; set; }
}