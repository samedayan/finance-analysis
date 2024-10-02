using Finance.Analysis.Contracts.Commands.WorkItems.Add;
using Finance.Analysis.Contracts.Commands.WorkItems.Responses;
using Finance.Analysis.Contracts.Commands.WorkItems.Update;
using Finance.Analysis.Contracts.Queries.WorkItems.Responses;
using Finance.Analysis.Contracts.Queries.WorkItems.Search;

namespace Finance.Analysis.Domain.Repositories.WorkItem;

public interface IWorkItemRepository
{
    public Task<FindWorkItemResponse> FindWorkItems(FindWorkItemQuery request);
    public Task<WorkItemResponse> Save(WorkItemAddCommand request, CancellationToken cancellationToken);
    public Task<WorkItemResponse> Update(WorkItemUpdateCommand request, CancellationToken cancellationToken);
    public Task<WorkItemResponse> Delete(Guid partnerId, CancellationToken cancellationToken);
}