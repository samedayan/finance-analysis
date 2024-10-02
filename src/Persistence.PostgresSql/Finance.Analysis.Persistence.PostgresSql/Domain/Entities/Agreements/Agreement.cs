using Finance.Analysis.Persistence.PostgresSql.Domain.Abstract;
using Finance.Analysis.Persistence.PostgresSql.Domain.Entities.Partners;
using Finance.Analysis.Persistence.PostgresSql.Domain.Entities.WorkItems;

namespace Finance.Analysis.Persistence.PostgresSql.Domain.Entities.Agreements;

public class Agreement : Entity
{
    public string Name { get; set; }
    public Guid PartnerId { get; set; } 
    public virtual Partner Partner { get; set; }
    public virtual ICollection<WorkItem> WorkItems { get; set; }
}