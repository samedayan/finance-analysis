using Finance.Analysis.Contracts.Queries.Agreement.ViewModels;

namespace Finance.Analysis.Contracts.Queries.Agreement.Responses;

public class FindAgreementResponse
{
    public List<FindAgreementViewModel> Data { get; set; } = [];
    public int TotalCount { get; set; }
}