using System.Linq.Expressions;
using Finance.Analysis.Contracts.Commands.RiskAnalysis.Add;
using Finance.Analysis.Contracts.Commands.RiskAnalysis.Responses;
using Finance.Analysis.Contracts.Commands.RiskAnalysis.Update;
using Finance.Analysis.Contracts.Queries.RiskAnalysis.Responses;
using Finance.Analysis.Contracts.Queries.RiskAnalysis.Search;
using Finance.Analysis.Contracts.Queries.RiskAnalysis.ViewModels;
using Finance.Analysis.Persistence.PostgresSql.DataAccess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Finance.Analysis.Domain.Repositories.RiskAnalysis;

public class RiskAnalysisRepository(IDbContextFactory<FinanceRiskAnalysisContext> dbContextFactory) : IRiskAnalysisRepository
{
        public async Task<RiskAnalysisResponse> Save(RiskAnalysisAddCommand item, CancellationToken cancellationToken)
    {
        var response = new RiskAnalysisResponse { Success = true };
        
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var map = item.Adapt<Persistence.PostgresSql.Domain.Entities.RiskAnalysis.RiskAnalysis>();

            dbContext.RiskAnalysis.Add(map);
        
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
    public async Task<FindRiskAnalysisResponse> FindRiskAnalysis(FindRiskAnalysisQuery request)
    {
        var response = new FindRiskAnalysisResponse();
        
        Expression<Func<Persistence.PostgresSql.Domain.Entities.RiskAnalysis.RiskAnalysis, bool>> predicate = x => true;

        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        
        var query = dbContext.RiskAnalysis
            //.Include(t => t.Partner)
            //.Include(t => t.RiskAnalysiss)
            .Where(predicate)
            //.WhereIf(!string.IsNullOrEmpty(request.PartnerId), t => t.Id == new Guid(request.PartnerId))
            .AsNoTracking();
        
        if (request.Page.HasValue) query = query.Skip((request.Page.Value - 1) * request.PageSize!.Value);

        if (request.PageSize.HasValue) query = query.Take(request.PageSize.Value);
        
        response.TotalCount = await query.CountAsync();
        
        var partners = await query.ToListAsync();
        
        response.Data = partners.Adapt<List<FindRiskAnalysisViewModel>>();
        
        return response;
    }
    public async Task<RiskAnalysisResponse> Update(RiskAnalysisUpdateCommand request, CancellationToken cancellationToken)
    {
        var response = new RiskAnalysisResponse { Success = true };
        
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var riskAnalysis = await dbContext.RiskAnalysis.FirstOrDefaultAsync(t => t.Id == request.RiskAnalysisId, cancellationToken);

            if (riskAnalysis is null || riskAnalysis.Id == Guid.Empty)
            {
                throw new KeyNotFoundException("Partner not found");
            }

            dbContext.RiskAnalysis.Update(riskAnalysis);
        
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
    public async Task<RiskAnalysisResponse> Delete(Guid partnerId, CancellationToken cancellationToken)
    {
        var response = new RiskAnalysisResponse { Success = true };
        
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var riskAnalysis = await dbContext.RiskAnalysis.FirstOrDefaultAsync(t => t.Id == partnerId, cancellationToken);

            if (riskAnalysis is null || riskAnalysis.Id == Guid.Empty)
            {
                throw new KeyNotFoundException("Partner not found");
            }

            riskAnalysis.AuditInformation.Status = 99;
            riskAnalysis.AuditInformation.UpdatedDate = DateTime.Now;
            riskAnalysis.AuditInformation.UpdatedUserId = 99;

            dbContext.RiskAnalysis.Update(riskAnalysis);
        
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