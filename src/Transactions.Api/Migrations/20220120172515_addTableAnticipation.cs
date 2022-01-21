using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transactions.Api.Migrations
{
    public partial class addTableAnticipation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "anticipation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    card_transaction_id = table.Column<int>(type: "int", nullable: false),
                    request_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    analysis_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    analysis_completion_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    result_analysis = table.Column<int>(type: "int", nullable: true),
                    request_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    anticipated_value = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_anticipation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_anticipation_card_transaction_card_transaction_id",
                        column: x => x.card_transaction_id,
                        principalTable: "card_transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_anticipation_card_transaction_id",
                table: "anticipation",
                column: "card_transaction_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "anticipation");
        }
    }
}
