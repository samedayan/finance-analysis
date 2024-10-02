using Finance.Analysis.Persistence.PostgresSql.Domain.Abstract;
using Finance.Analysis.Persistence.PostgresSql.Domain.Entities.Agreements;

namespace Finance.Analysis.Persistence.PostgresSql.Domain.Entities.WorkItems;

public class WorkItem : Entity
{
    public string Name { get; set; }
    public Guid AgreementId { get; set; }
    public virtual Agreement Agreement { get; set; }
    public virtual RiskAnalysis.RiskAnalysis RiskAnalysis { get; set; }
}