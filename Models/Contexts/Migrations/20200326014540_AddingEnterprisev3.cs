using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP_SPARTAN.Data.Migrations
{
    public partial class AddingEnterprisev3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Enterprises",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enterprises_UserId",
                table: "Enterprises",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enterprises_AspNetUsers_UserId",
                table: "Enterprises",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enterprises_AspNetUsers_UserId",
                table: "Enterprises");

            migrationBuilder.DropIndex(
                name: "IX_Enterprises_UserId",
                table: "Enterprises");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Enterprises");
        }
    }
}
