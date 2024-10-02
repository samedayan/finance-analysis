namespace Finance.Analysis.Infrastructure.ValueObjects;

public class AuditInformation
{
    public AuditInformation()
    {
        CreatedDate = DateTime.Now;
        CreateUserId = 1;
        Status = 0;
    }
    
    public int Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public int CreateUserId { get; set; }
    public int? UpdatedUserId { get; set; }
}