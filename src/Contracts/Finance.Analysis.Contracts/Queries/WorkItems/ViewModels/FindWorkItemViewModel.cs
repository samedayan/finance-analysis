using Finance.Analysis.Infrastructure.ValueObjects;

namespace Finance.Analysis.Contracts.Queries.WorkItems.ViewModels;

public class FindWorkItemViewModel
{
    public Guid Id { get; }
    public string Name { get; set; }
}

public class WorkItemDto1 : AuditInformation
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ContactEmail { get; set; } 
    public string ContactPhone { get; set; }  
}
