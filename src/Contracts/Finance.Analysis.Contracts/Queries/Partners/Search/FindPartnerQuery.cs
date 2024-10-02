using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Queries.Partners.Responses;

namespace Finance.Analysis.Contracts.Queries.Partners.Search;

public class FindPartnerQuery : IRequestWrapper<FindPartnerResponse>
{
    public int? Page { get; set; } = 1;
    public int? PageSize { get; set; } = 10;
    public string? PartnerId { get; set; }
}