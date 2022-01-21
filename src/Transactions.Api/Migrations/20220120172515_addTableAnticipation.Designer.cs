﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Transactions.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220120172515_addTableAnticipation")]
    partial class addTableAnticipation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Anticipation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("AnalysisCompletionDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("analysis_completion_date");

                    b.Property<DateTime>("AnalysisDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("analysis_date");

                    b.Property<decimal?>("AnticipatedValue")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("anticipated_value");

                    b.Property<int>("CardTransactionId")
                        .HasColumnType("int")
                        .HasColumnName("card_transaction_id");

                    b.Property<decimal>("RequestAmount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("request_amount");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("request_date");

                    b.Property<int?>("ResultAnalysis")
                        .HasColumnType("int")
                        .HasColumnName("result_analysis");

                    b.HasKey("Id")
                        .HasName("pk_anticipation");

                    b.HasIndex(new[] { "CardTransactionId" }, "ix_anticipation_card_transaction_id");

                    b.ToTable("anticipation", (string)null);
                });

            modelBuilder.Entity("CardTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AcquirerConfirmation")
                        .HasColumnType("int")
                        .HasColumnName("acquirer_confirmation");

                    b.Property<bool>("Anticipad")
                        .HasColumnType("bit")
                        .HasColumnName("anticipad");

                    b.Property<DateTime?>("ApprovalDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("approval_date");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)")
                        .HasColumnName("card_number");

                    b.Property<DateTime?>("DisapprovalDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("disapproval_date");

                    b.Property<decimal>("FixedFeeCharged")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("fixed_fee_charged");

                    b.Property<decimal>("GrossTransactionValue")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("gross_transaction_value");

                    b.Property<string>("LastFourDigitsCard")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)")
                        .HasColumnName("last_four_digits_card");

                    b.Property<decimal>("LiquidTransactionValue")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("liquid_transaction_value");

                    b.Property<int>("Nsu")
                        .HasColumnType("int")
                        .HasColumnName("nsu");

                    b.Property<int>("NumberParcel")
                        .HasColumnType("int")
                        .HasColumnName("number_parcel");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("transaction_date");

                    b.HasKey("Id")
                        .HasName("pk_card_transaction");

                    b.ToTable("card_transaction", (string)null);
                });

            modelBuilder.Entity("Parcel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CardTransactionId")
                        .HasColumnType("int")
                        .HasColumnName("card_transaction_id");

                    b.Property<DateTime>("ExpectedReceiptDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("expected_receipt_date");

                    b.Property<decimal>("GrossValue")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("gross_value");

                    b.Property<decimal>("LiquidValue")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("liquid_value");

                    b.Property<int>("NumberParcel")
                        .HasColumnType("int")
                        .HasColumnName("number_parcel");

                    b.Property<DateTime?>("TransferredParcelDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("transferred_parcel_date");

                    b.Property<decimal?>("ValueAnticipation")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("value_anticipation");

                    b.HasKey("Id")
                        .HasName("pk_parcel");

                    b.HasIndex(new[] { "CardTransactionId" }, "ix_parcels_card_transaction_id");

                    b.ToTable("parcel", (string)null);
                });

            modelBuilder.Entity("Anticipation", b =>
                {
                    b.HasOne("CardTransaction", "CardTransaction")
                        .WithMany("Anticipations")
                        .HasForeignKey("CardTransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CardTransaction");
                });

            modelBuilder.Entity("Parcel", b =>
                {
                    b.HasOne("CardTransaction", "CardTransaction")
                        .WithMany("Parcels")
                        .HasForeignKey("CardTransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CardTransaction");
                });

            modelBuilder.Entity("CardTransaction", b =>
                {
                    b.Navigation("Anticipations");

                    b.Navigation("Parcels");
                });
#pragma warning restore 612, 618
        }
    }
}
