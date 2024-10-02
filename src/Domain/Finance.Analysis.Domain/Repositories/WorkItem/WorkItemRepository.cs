using System.Linq.Expressions;
using Finance.Analysis.Contracts.Commands.Partners.Responses;
using Finance.Analysis.Contracts.Commands.Partners.Update;
using Finance.Analysis.Contracts.Commands.WorkItems.Add;
using Finance.Analysis.Contracts.Commands.WorkItems.Responses;
using Finance.Analysis.Contracts.Commands.WorkItems.Update;
using Finance.Analysis.Contracts.Queries.WorkItems.Responses;
using Finance.Analysis.Contracts.Queries.WorkItems.Search;
using Finance.Analysis.Contracts.Queries.WorkItems.ViewModels;
using Finance.Analysis.Persistence.PostgresSql.DataAccess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Finance.Analysis.Domain.Repositories.WorkItem;

public class WorkItemRepository(IDbContextFactory<FinanceRiskAnalysisContext> dbContextFactory) : IWorkItemRepository
{
    public async Task<WorkItemResponse> Save(WorkItemAddCommand item, CancellationToken cancellationToken)
    {
        var response = new WorkItemResponse { Success = true };
        
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var map = item.Adapt<Persistence.PostgresSql.Domain.Entities.WorkItems.WorkItem>();

            dbContext.WorkItems.Add(map);
        
            await dbContext.SaveChangesAsync(cancellationToken);
            
            await transaction.CommitAsync(cancellationToken);
        }
        catch 
        {
            response.Success = false;
            await transaction.RollbackAsync(cancellationToken);
        }

        return response;
    }
    public async Task<FindWorkItemResponse> FindWorkItems(FindWorkItemQuery request)
    {
        var response = new FindWorkItemResponse();
        
        Expression<Func<Persistence.PostgresSql.Domain.Entities.WorkItems.WorkItem, bool>> predicate = x => true;

        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        
        var query = dbContext.WorkItems
            //.Include(t => t.Partner)
            //.Include(t => t.WorkItems)
            .Where(predicate)
            //.WhereIf(!string.IsNullOrEmpty(request.PartnerId), t => t.Id == new Guid(request.PartnerId))
            .AsNoTracking();
        
        if (request.Page.HasValue) query = query.Skip((request.Page.Value - 1) * request.PageSize!.Value);

        if (request.PageSize.HasValue) query = query.Take(request.PageSize.Value);
        
        response.TotalCount = await query.CountAsync();
        
        var partners = await query.ToListAsync();
        
        response.Data = partners.Adapt<List<FindWorkItemViewModel>>();
        
        return response;
    }
    public async Task<WorkItemResponse> Update(WorkItemUpdateCommand request, CancellationToken cancellationToken)
    {
        var response = new WorkItemResponse { Success = true };
        
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var workItem = await dbContext.WorkItems.FirstOrDefaultAsync(t => t.Id == request.WorkItemId, cancellationToken);

            if (workItem is null || workItem.Id == Guid.Empty)
            {
                throw new KeyNotFoundException("Partner not found");
            }

            dbContext.WorkItems.Update(workItem);
        
            await dbContext.SaveChangesAsync(cancellationToken);
            
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            response.Success = false;
            await transaction.RollbackAsync(cancellationToken);
        }

        return response;
        
    }
    public async Task<WorkItemResponse> Delete(Guid partnerId, CancellationToken cancellationToken)
    {
        var response = new WorkItemResponse { Success = true };
        
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var workItem = await dbContext.WorkItems.FirstOrDefaultAsync(t => t.Id == partnerId, cancellationToken);

            if (workItem is null || workItem.Id == Guid.Empty)
            {
                throw new KeyNotFoundException("Partner not found");
            }

            workItem.AuditInformation.Status = 99;
            workItem.AuditInformation.UpdatedDate = DateTime.Now;
            workItem.AuditInformation.UpdatedUserId = 99;

            dbContext.WorkItems.Update(workItem);
        
            await dbContext.SaveChangesAsync(cancellationToken);
            
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            response.Success = false;
            await transaction.RollbackAsync(cancellationToken);
        }

        return response;
        
    }
}