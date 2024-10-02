using Finance.Analysis.Contracts.CommandQueryWrappers;
using Finance.Analysis.Contracts.Commands.WorkItems.Responses;
using Finance.Analysis.Contracts.Commands.WorkItems.Update;
using Finance.Analysis.Domain.Repositories.WorkItem;

namespace Finance.Analysis.Application.Cqrs.Commands.WorkItems.Update;

public class WorkItemUpdateCommandHandler(IWorkItemRepository workItemRepository) : IRequestHandlerWrapper<WorkItemUpdateCommand, WorkItemResponse>
{
    public async Task<WorkItemResponse> Handle(WorkItemUpdateCommand request, CancellationToken cancellationToken)
    {
        return await workItemRepository.Update(request, cancellationToken);
    }
}