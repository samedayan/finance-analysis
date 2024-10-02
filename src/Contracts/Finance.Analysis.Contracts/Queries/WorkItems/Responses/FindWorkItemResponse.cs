using Finance.Analysis.Contracts.Queries.WorkItems.ViewModels;

namespace Finance.Analysis.Contracts.Queries.WorkItems.Responses;

public class FindWorkItemResponse
{
    public List<FindWorkItemViewModel> Data { get; set; } = [];
    public int TotalCount { get; set; }
}