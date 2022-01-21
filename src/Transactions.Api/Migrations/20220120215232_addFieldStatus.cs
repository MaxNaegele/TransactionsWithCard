using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transactions.Api.Migrations
{
    public partial class addFieldStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status_anticipation",
                table: "anticipation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status_anticipation",
                table: "anticipation");
        }
    }
}
