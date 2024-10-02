using Finance.Analysis.Persistence.PostgresSql.Constants;
using Finance.Analysis.Persistence.PostgresSql.Domain.Entities.Agreements;
using Finance.Analysis.Persistence.PostgresSql.Domain.Entities.Partners;
using Finance.Analysis.Persistence.PostgresSql.Domain.Entities.RiskAnalysis;
using Finance.Analysis.Persistence.PostgresSql.Domain.Entities.WorkItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Finance.Analysis.Persistence.PostgresSql.DataAccess;

public class FinanceRiskAnalysisContext : DbContext
{
   /// <summary>
  /// </summary>
  /// <param name="options"></param>
  public FinanceRiskAnalysisContext(DbContextOptions<FinanceRiskAnalysisContext> options) : base(options)
  {
      AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
  }
   
  public DbSet<Agreement> Agreements { get; set; }
  public DbSet<Partner> Partners { get; set; }
  public DbSet<RiskAnalysis> RiskAnalysis { get; set; }
  public DbSet<WorkItem> WorkItems { get; set; }
  
  /// <summary>
  /// </summary>
  /// <param name="optionsBuilder"></param>
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
      if (optionsBuilder.IsConfigured) return;

      optionsBuilder.UseNpgsql(string.Empty, builder => builder.EnableRetryOnFailure(3)).EnableSensitiveDataLogging();
      optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
  }

  /// <summary>
  /// </summary>
  /// <param name="modelBuilder"></param>
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
      modelBuilder.HasDefaultSchema(SchemaNames.FinanceAnalysisDatabaseContextSchemaName);

      #region Agreement
      modelBuilder.Entity<Agreement>().ToTable("agreement").HasKey(t => t.Id);
      modelBuilder.Entity<Agreement>().Property(t => t.Id).HasColumnName("id");
      modelBuilder.Entity<Agreement>().Property(t => t.Name).HasColumnName("name");
      modelBuilder.Entity<Agreement>().Property(t => t.PartnerId).HasColumnName("partner_id");
      modelBuilder.Entity<Agreement>().OwnsOne(t => t.AuditInformation).Property(t => t.Status).HasColumnName("status");
      modelBuilder.Entity<Agreement>().OwnsOne(t => t.AuditInformation).Property(t => t.CreatedDate).HasColumnName("create_date");
      modelBuilder.Entity<Agreement>().OwnsOne(t => t.AuditInformation).Property(t => t.UpdatedDate).HasColumnName("update_date");
      modelBuilder.Entity<Agreement>().OwnsOne(t => t.AuditInformation).Property(t => t.CreateUserId).HasColumnName("create_user_id");
      modelBuilder.Entity<Agreement>().OwnsOne(t => t.AuditInformation).Property(t => t.UpdatedUserId).HasColumnName("update_user_id");
      #endregion
      
      #region Partner
      modelBuilder.Entity<Partner>().ToTable("partner").HasKey(t => t.Id);
      modelBuilder.Entity<Partner>().Property(t => t.Id).HasColumnName("id");
      modelBuilder.Entity<Partner>().Property(t => t.Name).HasColumnName("name");
      modelBuilder.Entity<Partner>().Property(t => t.ContactEmail).HasColumnName("contact_email");
      modelBuilder.Entity<Partner>().Property(t => t.ContactPhone).HasColumnName("contact_phone");
      modelBuilder.Entity<Partner>().OwnsOne(t => t.AuditInformation).Property(t => t.Status).HasColumnName("status");
      modelBuilder.Entity<Partner>().OwnsOne(t => t.AuditInformation).Property(t => t.CreatedDate).HasColumnName("create_date");
      modelBuilder.Entity<Partner>().OwnsOne(t => t.AuditInformation).Property(t => t.UpdatedDate).HasColumnName("update_date");
      modelBuilder.Entity<Partner>().OwnsOne(t => t.AuditInformation).Property(t => t.CreateUserId).HasColumnName("create_user_id");
      modelBuilder.Entity<Partner>().OwnsOne(t => t.AuditInformation).Property(t => t.UpdatedUserId).HasColumnName("update_user_id");
      #endregion
      
      #region RiskAnalysis
      modelBuilder.Entity<RiskAnalysis>().ToTable("risk_analysis").HasKey(t => t.Id);
      modelBuilder.Entity<RiskAnalysis>().Property(t => t.Id).HasColumnName("id");
      modelBuilder.Entity<RiskAnalysis>().Property(t => t.WorkItemId).HasColumnName("work_item_id");
      modelBuilder.Entity<RiskAnalysis>().Property(t => t.CalculatedRisk).HasColumnName("calculated_risk");
      modelBuilder.Entity<RiskAnalysis>().OwnsOne(t => t.AuditInformation).Property(t => t.Status).HasColumnName("status");
      modelBuilder.Entity<RiskAnalysis>().OwnsOne(t => t.AuditInformation).Property(t => t.CreatedDate).HasColumnName("create_date");
      modelBuilder.Entity<RiskAnalysis>().OwnsOne(t => t.AuditInformation).Property(t => t.UpdatedDate).HasColumnName("update_date");
      modelBuilder.Entity<RiskAnalysis>().OwnsOne(t => t.AuditInformation).Property(t => t.CreateUserId).HasColumnName("create_user_id");
      modelBuilder.Entity<RiskAnalysis>().OwnsOne(t => t.AuditInformation).Property(t => t.UpdatedUserId).HasColumnName("update_user_id");
      #endregion
      
      #region WorkItem
      modelBuilder.Entity<WorkItem>().ToTable("work_item").HasKey(t => t.Id);
      modelBuilder.Entity<WorkItem>().Property(t => t.Id).HasColumnName("id");
      modelBuilder.Entity<WorkItem>().Property(t => t.Name).HasColumnName("name");
      modelBuilder.Entity<WorkItem>().Property(t => t.AgreementId).HasColumnName("agreement_id");
      modelBuilder.Entity<WorkItem>().OwnsOne(t => t.AuditInformation).Property(t => t.Status).HasColumnName("status");
      modelBuilder.Entity<WorkItem>().OwnsOne(t => t.AuditInformation).Property(t => t.CreatedDate).HasColumnName("create_date");
      modelBuilder.Entity<WorkItem>().OwnsOne(t => t.AuditInformation).Property(t => t.UpdatedDate).HasColumnName("update_date");
      modelBuilder.Entity<WorkItem>().OwnsOne(t => t.AuditInformation).Property(t => t.CreateUserId).HasColumnName("create_user_id");
      modelBuilder.Entity<WorkItem>().OwnsOne(t => t.AuditInformation).Property(t => t.UpdatedUserId).HasColumnName("update_user_id");
      #endregion
  }
}