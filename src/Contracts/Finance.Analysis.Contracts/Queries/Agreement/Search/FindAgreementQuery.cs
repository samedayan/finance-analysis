using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Queries.Agreement.Responses;

namespace Finance.Analysis.Contracts.Queries.Agreement.Search;

public class FindAgreementQuery : IRequestWrapper<FindAgreementResponse>
{
    public int? Page { get; set; } = 1;
    public int? PageSize { get; set; } = 10;
    public string? AgreementId { get; set; }
}