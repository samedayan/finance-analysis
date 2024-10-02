using Finance.Analysis.Infrastructure.ValueObjects;

namespace Finance.Analysis.Persistence.PostgresSql.Domain.Abstract;

public interface IAuditableEntity
{
    public AuditInformation AuditInformation { get; set; }
}