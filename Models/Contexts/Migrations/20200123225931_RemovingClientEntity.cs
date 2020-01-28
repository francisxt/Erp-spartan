using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP_SPARTAN.Data.Migrations
{
    public partial class RemovingClientEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_SubClients_ClientId",
                table: "Movements");

            migrationBuilder.DropTable(
                name: "SubClients");

            migrationBuilder.DropIndex(
                name: "IX_Movements_ClientId",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Movements");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientUserId",
                table: "Movements",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Movements_ClientUserId",
                table: "Movements",
                column: "ClientUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_ClientUsers_ClientUserId",
                table: "Movements",
                column: "ClientUserId",
                principalTable: "ClientUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_ClientUsers_ClientUserId",
                table: "Movements");

            migrationBuilder.DropIndex(
                name: "IX_Movements_ClientUserId",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "ClientUserId",
                table: "Movements");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "Movements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SubClients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdentificationCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubClients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubClients_ClientUsers_ClientUserId",
                        column: x => x.ClientUserId,
                        principalTable: "ClientUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubClients_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movements_ClientId",
                table: "Movements",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SubClients_ClientUserId",
                table: "SubClients",
                column: "ClientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubClients_UserId",
                table: "SubClients",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_SubClients_ClientId",
                table: "Movements",
                column: "ClientId",
                principalTable: "SubClients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
