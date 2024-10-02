using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Commands.Partners.Responses;

namespace Finance.Analysis.Contracts.Commands.Partners.Delete;

public class PartnerDeleteCommand : IRequestWrapper<PartnerResponse>
{
    public string PartnerId { get; set; }
}