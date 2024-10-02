using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Commands.Agreements.Responses;

namespace Finance.Analysis.Contracts.Commands.Agreements.Update;

public class AgreementUpdateCommand : IRequestWrapper<AgreementResponse>
{
    public Guid AgreementId { get; set; }
}