using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Commands.Partners.Responses;

namespace Finance.Analysis.Contracts.Commands.Partners.Add;

public class PartnerAddCommand : IRequestWrapper<PartnerResponse>
{
    public string Name { get; set; }
    public string ContactEmail { get; set; }
    public string ContactPhone { get; set; }
}