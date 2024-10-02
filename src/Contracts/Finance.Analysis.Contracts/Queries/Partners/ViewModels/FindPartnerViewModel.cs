using Finance.Analysis.Infrastructure.ValueObjects;

namespace Finance.Analysis.Contracts.Queries.Partners.ViewModels;

public class FindPartnerViewModel : AuditInformation
{
    public Guid Id { get; set;  }
    public string Name { get; set; }
    public string ContactEmail { get; set; } 
    public string ContactPhone { get; set; }  
    public string StatusName { get; set; }
}
