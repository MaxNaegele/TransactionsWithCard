using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transactions.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "card_transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    card_number = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    transaction_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    approval_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    disapproval_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    anticipad = table.Column<bool>(type: "bit", nullable: false),
                    acquirer_confirmation = table.Column<int>(type: "int", nullable: false),
                    gross_transaction_value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    liquid_transaction_value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    fixed_fee_charged = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    number_parcel = table.Column<int>(type: "int", nullable: false),
                    last_four_digits_card = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_card_transaction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "parcel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    card_transaction_id = table.Column<int>(type: "int", nullable: false),
                    gross_value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    liquid_value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    number_parcel = table.Column<int>(type: "int", nullable: false),
                    value_anticipation = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    expected_receipt_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    transferred_parcel_date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_parcel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_parcel_card_transaction_card_transaction_id",
                        column: x => x.card_transaction_id,
                        principalTable: "card_transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_parcels_card_transaction_id",
                table: "parcel",
                column: "card_transaction_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "parcel");

            migrationBuilder.DropTable(
                name: "card_transaction");
        }
    }
}
