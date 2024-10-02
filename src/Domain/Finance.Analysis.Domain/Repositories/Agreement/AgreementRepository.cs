using System.Linq.Expressions;
using Finance.Analysis.Contracts.Commands.Agreements.Add;
using Finance.Analysis.Contracts.Commands.Agreements.Responses;
using Finance.Analysis.Contracts.Commands.Agreements.Update;
using Finance.Analysis.Contracts.Queries.Agreement.Responses;
using Finance.Analysis.Contracts.Queries.Agreement.Search;
using Finance.Analysis.Contracts.Queries.Agreement.ViewModels;
using Finance.Analysis.Domain.Extensions;
using Finance.Analysis.Persistence.PostgresSql.DataAccess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Finance.Analysis.Domain.Repositories.Agreement;

public class AgreementRepository(IDbContextFactory<FinanceRiskAnalysisContext> dbContextFactory) : IAgreementRepository
{
    public async Task<FindAgreementResponse> FindAgreements(FindAgreementQuery request)
    {
        var response = new FindAgreementResponse();
        
        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        
        var query = dbContext.Agreements
            .Include(t => t.Partner)
            .Include(t => t.WorkItems)
            .ThenInclude(t => t.RiskAnalysis)
            .WhereIf(!string.IsNullOrEmpty(request.AgreementId), t => t.Id == new Guid(request.AgreementId))
            .AsNoTracking();
        
        if (request.Page.HasValue) query = query.Skip((request.Page.Value - 1) * request.PageSize!.Value);

        if (request.PageSize.HasValue) query = query.Take(request.PageSize.Value);
        
        response.TotalCount = await query.CountAsync();
        
        var agreements = await query.ToListAsync();
        
        response.Data = agreements.Adapt<List<FindAgreementViewModel>>();
        
        return response;
    }
    public async Task<AgreementResponse> Save(AgreementAddCommand request, CancellationToken cancellationToken)
    {
        var response = new AgreementResponse { Success = true };
        
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var map = request.Adapt<Persistence.PostgresSql.Domain.Entities.Agreements.Agreement>();

            dbContext.Agreements.Add(map);
        
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
    public async Task<AgreementResponse> Update(AgreementUpdateCommand request, CancellationToken cancellationToken)
    {
        var response = new AgreementResponse { Success = true };
        
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var agreement = await dbContext.Agreements.FirstOrDefaultAsync(t => t.Id == request.AgreementId, cancellationToken);

            if (agreement is null || agreement.Id == Guid.Empty)
            {
                throw new KeyNotFoundException("Agreement not found");
            }

            dbContext.Agreements.Update(agreement);
        
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
    
    public async Task<AgreementResponse> Delete(Guid agreementId, CancellationToken cancellationToken)
    {
        var response = new AgreementResponse { Success = true };
        
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var agreement = await dbContext.Agreements.FirstOrDefaultAsync(t => t.Id == agreementId, cancellationToken);

            if (agreement is null || agreement.Id == Guid.Empty)
            {
                throw new KeyNotFoundException("Agreement not found");
            }

            agreement.AuditInformation.Status = 99;
            agreement.AuditInformation.UpdatedDate = DateTime.Now;
            agreement.AuditInformation.UpdatedUserId = 99;

            dbContext.Agreements.Update(agreement);
        
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