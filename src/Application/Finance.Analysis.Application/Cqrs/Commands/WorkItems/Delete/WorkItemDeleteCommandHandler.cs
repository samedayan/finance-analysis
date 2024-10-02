using Finance.Analysis.Contracts.Commands.Partners.Responses;
using Finance.Analysis.Contracts.Commands.WorkItems.Responses;
using Finance.Analysis.Domain.Repositories.Partner;
using Finance.Analysis.Domain.Repositories.WorkItem;

namespace Finance.Analysis.Application.Cqrs.Commands.WorkItems.Delete;

public class WorkITemDeleteCommandHandler(IWorkItemRepository workItemRepository)
{
    public async Task<WorkItemResponse> Handle(Guid partnerId, CancellationToken cancellationToken)
    {
        return await workItemRepository.Delete(partnerId, cancellationToken);
    }
}