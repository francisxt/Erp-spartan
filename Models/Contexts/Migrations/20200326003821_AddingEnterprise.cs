using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP_SPARTAN.Data.Migrations
{
    public partial class AddingEnterprise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnterpriseName",
                table: "ClientUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "EnterpriseId",
                table: "ClientUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Enterprise",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: false),
                    State = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprise", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientUsers_EnterpriseId",
                table: "ClientUsers",
                column: "EnterpriseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientUsers_Enterprise_EnterpriseId",
                table: "ClientUsers",
                column: "EnterpriseId",
                principalTable: "Enterprise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientUsers_Enterprise_EnterpriseId",
                table: "ClientUsers");

            migrationBuilder.DropTable(
                name: "Enterprise");

            migrationBuilder.DropIndex(
                name: "IX_ClientUsers_EnterpriseId",
                table: "ClientUsers");

            migrationBuilder.DropColumn(
                name: "EnterpriseId",
                table: "ClientUsers");

            migrationBuilder.AddColumn<string>(
                name: "EnterpriseName",
                table: "ClientUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
