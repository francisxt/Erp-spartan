using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP_SPARTAN.Data.Migrations
{
    public partial class AddingIdentificationCardInClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentificationCard",
                table: "ClientUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentificationCard",
                table: "ClientUsers");
        }
    }
}
