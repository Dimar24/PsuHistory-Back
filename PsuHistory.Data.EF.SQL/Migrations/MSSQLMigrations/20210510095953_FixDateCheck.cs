using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PsuHistory.Data.EF.SQL.Migrations.MSSQLMigrations
{
    public partial class FixDateCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AttachmentBurials",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 10, 12, 59, 53, 521, DateTimeKind.Local).AddTicks(6828),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AttachmentBurials",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 5, 10, 12, 59, 53, 513, DateTimeKind.Local).AddTicks(3551),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "AttachmentBurials",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 10, 12, 59, 53, 521, DateTimeKind.Local).AddTicks(6828));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "AttachmentBurials",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 5, 10, 12, 59, 53, 513, DateTimeKind.Local).AddTicks(3551));
        }
    }
}
