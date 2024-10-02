using Finance.Analysis.Persistence.PostgresSql.Domain.Abstract;
using Finance.Analysis.Persistence.PostgresSql.Domain.Entities.WorkItems;

namespace Finance.Analysis.Persistence.PostgresSql.Domain.Entities.RiskAnalysis;

public class RiskAnalysis : Entity
{
    public Guid WorkItemId { get; set; }
    public decimal CalculatedRisk { get; set; }
    public virtual WorkItem WorkItem { get; set; }
}