using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class AnticipationMap
{
    public static void Configure(this EntityTypeBuilder<Anticipation> entity)
    {
        entity.ToTable("anticipation");
        
        entity.HasKey(e => e.Id)
        .HasName("pk_anticipation");

        entity.HasIndex(e => e.CardTransactionId, "ix_anticipation_card_transaction_id");

        entity.Property(e => e.CardTransactionId)
        .HasColumnName("card_transaction_id");
    
        entity.Property(e => e.RequestDate)
        .HasColumnName("request_date");

        entity.Property(e => e.AnalysisDate)
        .HasColumnName("analysis_date");

        entity.Property(e => e.AnalysisCompletionDate)
        .HasColumnName("analysis_completion_date");

        entity.Property(e => e.ResultAnalysis)
        .HasColumnName("result_analysis");

        entity.Property(e => e.RequestAmount)
        .HasColumnType("decimal(18, 2)")
        .HasColumnName("request_amount");

        entity.Property(e => e.AnticipatedValue)
        .HasColumnType("decimal(18, 2)")
        .HasColumnName("anticipated_value");

        entity.Property(e => e.StatusAnticipation)
        .HasColumnName("status_anticipation");

        entity.HasOne(d => d.CardTransaction)
            .WithMany(p => p.Anticipations)
            .HasForeignKey(d => d.CardTransactionId);
    }

}