using Finance.Analysis.Infrastructure.ValueObjects;

namespace Finance.Analysis.Contracts.Queries.Agreement.ViewModels;

public class FindAgreementViewModel
{
    public Guid Id { get; }
    public string Name { get; set; }
    public PartnerDto Partner { get; set; }
    public List<WorkItemDto> WorkItems { get; set; } = [];
}

public class PartnerDto : AuditInformation
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ContactEmail { get; set; } 
    public string ContactPhone { get; set; }  
}

public class WorkItemDto : AuditInformation
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid AgreementId { get; set; }
    public RiskAnalysisDto RiskAnalysis { get; set; }
}

public class RiskAnalysisDto : AuditInformation
{
    public Guid WorkItemId { get; set; }
    public decimal CalculatedRisk { get; set; }
}