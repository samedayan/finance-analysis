using System.Linq.Expressions;
using Finance.Analysis.Contracts.Commands.Partners.Add;
using Finance.Analysis.Contracts.Commands.Partners.Responses;
using Finance.Analysis.Contracts.Commands.Partners.Update;
using Finance.Analysis.Contracts.Queries.Partners.Responses;
using Finance.Analysis.Contracts.Queries.Partners.Search;
using Finance.Analysis.Contracts.Queries.Partners.ViewModels;
using Finance.Analysis.Infrastructure.Exceptions;
using Finance.Analysis.Persistence.PostgresSql.DataAccess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Finance.Analysis.Domain.Repositories.Partner;

public class PartnerRepository(IDbContextFactory<FinanceRiskAnalysisContext> dbContextFactory) : IPartnerRepository
{
    public async Task<FindPartnerResponse> FindPartners(FindPartnerQuery request)
    {
        var response = new FindPartnerResponse();
        
        Expression<Func<Persistence.PostgresSql.Domain.Entities.Partners.Partner, bool>> predicate = x => true;

        await using var dbContext = await dbContextFactory.CreateDbContextAsync();
        
        var query = dbContext.Partners
            .Where(predicate)
            .AsNoTracking();
        
        if (request.Page.HasValue) query = query.Skip((request.Page.Value - 1) * request.PageSize!.Value);

        if (request.PageSize.HasValue) query = query.Take(request.PageSize.Value);
        
        response.TotalCount = await query.CountAsync();
        
        var partners = await query.ToListAsync();
        
        response.Data = partners.Adapt<List<FindPartnerViewModel>>();
        
        return response;
    }
    public async Task<PartnerResponse> Save(PartnerAddCommand request, CancellationToken cancellationToken)
    {
        var response = new PartnerResponse { Success = true };
        
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        var strategy = dbContext.Database.CreateExecutionStrategy();
        
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            
            try
            {
                var map = request.Adapt<Persistence.PostgresSql.Domain.Entities.Partners.Partner>();

                dbContext.Partners.Add(map);
        
                await dbContext.SaveChangesAsync(cancellationToken);
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                await transaction.RollbackAsync(cancellationToken);
            }
        
            await transaction.CommitAsync(cancellationToken);

        });

        return response;
        
    }
    public async Task<PartnerResponse> Update(PartnerUpdateCommand request, CancellationToken cancellationToken)
    {
        var response = new PartnerResponse { Success = true };
        
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var partner = await dbContext.Partners.FirstOrDefaultAsync(t => t.Id == request.PartnerId, cancellationToken);

            if (partner is null || partner.Id == Guid.Empty)
            {
                throw new KeyNotFoundException("Partner not found");
            }

            dbContext.Partners.Update(partner);
        
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
    public async Task<PartnerResponse> Delete(Guid partnerId, CancellationToken cancellationToken)
    {
        var response = new PartnerResponse { Success = true };
        
        await using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
        
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var partner = await dbContext.Partners.FirstOrDefaultAsync(t => t.Id == partnerId, cancellationToken);

            if (partner is null || partner.Id == Guid.Empty)
            {
                throw new KeyNotFoundException("Partner not found");
            }

            partner.AuditInformation.Status = 99;
            partner.AuditInformation.UpdatedDate = DateTime.Now;
            partner.AuditInformation.UpdatedUserId = 99;

            dbContext.Partners.Update(partner);
        
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