using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP_SPARTAN.Data.Migrations
{
    public partial class AddingEnterprisev2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientUsers_Enterprise_EnterpriseId",
                table: "ClientUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enterprise",
                table: "Enterprise");

            migrationBuilder.RenameTable(
                name: "Enterprise",
                newName: "Enterprises");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enterprises",
                table: "Enterprises",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientUsers_Enterprises_EnterpriseId",
                table: "ClientUsers",
                column: "EnterpriseId",
                principalTable: "Enterprises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientUsers_Enterprises_EnterpriseId",
                table: "ClientUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enterprises",
                table: "Enterprises");

            migrationBuilder.RenameTable(
                name: "Enterprises",
                newName: "Enterprise");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enterprise",
                table: "Enterprise",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientUsers_Enterprise_EnterpriseId",
                table: "ClientUsers",
                column: "EnterpriseId",
                principalTable: "Enterprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
