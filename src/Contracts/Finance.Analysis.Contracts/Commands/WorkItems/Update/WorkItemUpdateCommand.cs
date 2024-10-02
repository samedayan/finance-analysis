using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Commands.WorkItems.Responses;

namespace Finance.Analysis.Contracts.Commands.WorkItems.Update;

public class WorkItemUpdateCommand : IRequestWrapper<WorkItemResponse>
{
    public Guid WorkItemId { get; set; }
}