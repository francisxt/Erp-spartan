using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP_SPARTAN.Data.Migrations
{
    public partial class AddingStateOfUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "AspNetUsers");
        }
    }
}
