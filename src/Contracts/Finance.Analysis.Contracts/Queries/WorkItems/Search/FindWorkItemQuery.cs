using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Queries.WorkItems.Responses;

namespace Finance.Analysis.Contracts.Queries.WorkItems.Search;

public class FindWorkItemQuery : IRequestWrapper<FindWorkItemResponse>
{
    public int? Page { get; set; } = 1;
    public int? PageSize { get; set; } = 10;
    public string WorkItemId { get; set; }
}