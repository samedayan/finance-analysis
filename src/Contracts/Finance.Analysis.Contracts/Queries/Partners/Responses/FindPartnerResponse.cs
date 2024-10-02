using Finance.Analysis.Contracts.Queries.Partners.ViewModels;

namespace Finance.Analysis.Contracts.Queries.Partners.Responses;

public class FindPartnerResponse
{
    public List<FindPartnerViewModel> Data { get; set; } = [];
    public int TotalCount { get; set; }
}