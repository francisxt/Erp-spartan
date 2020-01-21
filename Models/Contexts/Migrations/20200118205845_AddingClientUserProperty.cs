using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP_SPARTAN.Data.Migrations
{
    public partial class AddingClientUserProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ClientUsers",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ClientUsers");
        }
    }
}
