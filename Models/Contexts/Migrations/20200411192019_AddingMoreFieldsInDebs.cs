using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP_SPARTAN.Data.Migrations
{
    public partial class AddingMoreFieldsInDebs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentCapital",
                table: "Debs",
                newName: "EndBalance");

            migrationBuilder.AddColumn<decimal>(
                name: "Amortitation",
                table: "Debs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Share",
                table: "Debs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amortitation",
                table: "Debs");

            migrationBuilder.DropColumn(
                name: "Share",
                table: "Debs");

            migrationBuilder.RenameColumn(
                name: "EndBalance",
                table: "Debs",
                newName: "PaymentCapital");
        }
    }
}
