using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class ParcelMap
{
    public static void Configure(this EntityTypeBuilder<Parcel> entity)
    {
        entity.ToTable("parcel");
        
        entity.HasKey(e => e.Id)
        .HasName("pk_parcel");

        entity.HasIndex(e => e.CardTransactionId, "ix_parcels_card_transaction_id");

        entity.Property(e => e.CardTransactionId)
        .HasColumnName("card_transaction_id");

        entity.Property(e => e.GrossValue)
        .HasColumnType("decimal(18, 2)")
        .HasColumnName("gross_value");

        entity.Property(e => e.LiquidValue)
        .HasColumnType("decimal(18, 2)")
        .HasColumnName("liquid_value");

        entity.Property(e => e.NumberParcel)
        .HasColumnName("number_parcel");

        entity.Property(e => e.ValueAnticipation)
        .HasColumnType("decimal(18, 2)")
        .HasColumnName("value_anticipation");

        entity.Property(e => e.ExpectedReceiptDate)
        .HasColumnName("expected_receipt_date");

        entity.Property(e => e.TransferredParcelDate)
        .HasColumnName("transferred_parcel_date");

        entity.HasOne(d => d.CardTransaction)
            .WithMany(p => p.Parcels)
            .HasForeignKey(d => d.CardTransactionId);
    }

}