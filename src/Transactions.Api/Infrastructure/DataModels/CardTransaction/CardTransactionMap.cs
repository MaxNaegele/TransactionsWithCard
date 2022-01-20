using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class CardTransactionMap
{
    public static void Configure(this EntityTypeBuilder<CardTransaction> entity)
    {
        entity.ToTable("card_transaction");

        entity.HasKey(e => e.Id)
        .HasName("pk_card_transaction");

        entity.Property(e => e.Nsu)
          .HasColumnName("nsu");

        entity.Property(e => e.CardNumber)
        .HasColumnName("card_number")
        .HasMaxLength(16);

        entity.Property(e => e.TransactionDate)
        .HasColumnName("transaction_date");

        entity.Property(e => e.ApprovalDate)
        .HasColumnName("approval_date");

        entity.Property(e => e.DisapprovalDate)
        .HasColumnName("disapproval_date");

        entity.Property(e => e.Anticipad)
        .HasColumnName("anticipad");

        entity.Property(e => e.AcquirerConfirmation)
        .HasColumnName("acquirer_confirmation");

        entity.Property(e => e.GrossTransactionValue)
        .HasColumnType("decimal(18, 2)")
        .HasColumnName("gross_transaction_value");

        entity.Property(e => e.LiquidTransactionValue)
        .HasColumnType("decimal(18, 2)")
        .HasColumnName("liquid_transaction_value");

        entity.Property(e => e.FixedFeeCharged)
        .HasColumnType("decimal(18, 2)")
        .HasColumnName("fixed_fee_charged");

        entity.Property(e => e.NumberParcel)
       .HasColumnName("number_parcel");

        entity.Property(e => e.LastFourDigitsCard)
       .HasColumnName("last_four_digits_card")
       .HasMaxLength(4);
    }

}