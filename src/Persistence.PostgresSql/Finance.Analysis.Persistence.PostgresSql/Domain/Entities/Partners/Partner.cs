using Finance.Analysis.Persistence.PostgresSql.Domain.Abstract;
using Finance.Analysis.Persistence.PostgresSql.Domain.Entities.Agreements;

namespace Finance.Analysis.Persistence.PostgresSql.Domain.Entities.Partners;

public class Partner : Entity
{
    public string Name { get; set; }
    public string ContactEmail { get; set; } 
    public string ContactPhone { get; set; }  
    public virtual ICollection<Agreement> Agreements { get; set; }

}