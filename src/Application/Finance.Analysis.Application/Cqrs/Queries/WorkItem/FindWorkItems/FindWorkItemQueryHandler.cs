using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Queries.WorkItems.Responses;
using Finance.Analysis.Contracts.Queries.WorkItems.Search;
using Finance.Analysis.Domain.Repositories.WorkItem;

namespace Finance.Analysis.Application.Cqrs.Queries.WorkItem.FindWorkItems;

public class FindWorkItemQueryHandler(IWorkItemRepository workItemRepository) : IRequestHandlerWrapper<FindWorkItemQuery, FindWorkItemResponse>
{
    public async Task<FindWorkItemResponse> Handle(FindWorkItemQuery request, CancellationToken cancellationToken)
    {
       return await workItemRepository.FindWorkItems(request);
    }
}