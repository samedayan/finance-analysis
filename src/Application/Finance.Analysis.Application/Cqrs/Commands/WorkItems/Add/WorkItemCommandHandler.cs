using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Commands.WorkItems.Add;
using Finance.Analysis.Contracts.Commands.WorkItems.Responses;
using Finance.Analysis.Domain.Repositories.WorkItem;

namespace Finance.Analysis.Application.Cqrs.Commands.WorkItems.Add;

public class WorkItemCommandHandler(IWorkItemRepository workItemRepository) : IRequestHandlerWrapper<WorkItemAddCommand, WorkItemResponse>
{
    public async Task<WorkItemResponse> Handle(WorkItemAddCommand request, CancellationToken cancellationToken)
    {
        return await workItemRepository.Save(request, cancellationToken);
    }
}