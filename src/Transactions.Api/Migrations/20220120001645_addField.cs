using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transactions.Api.Migrations
{
    public partial class addField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "nsu",
                table: "card_transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nsu",
                table: "card_transaction");
        }
    }
}
