using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PsuHistory.Data.EF.SQL.Migrations.MSSQLMigrations
{
    public partial class FixForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentBurials_Victims_VictimId",
                table: "AttachmentBurials");

            migrationBuilder.DropIndex(
                name: "IX_AttachmentBurials_VictimId",
                table: "AttachmentBurials");

            migrationBuilder.DropColumn(
                name: "VictimId",
                table: "AttachmentBurials");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VictimId",
                table: "AttachmentBurials",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentBurials_VictimId",
                table: "AttachmentBurials",
                column: "VictimId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentBurials_Victims_VictimId",
                table: "AttachmentBurials",
                column: "VictimId",
                principalTable: "Victims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
