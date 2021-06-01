using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PsuHistory.Data.EF.SQL.Migrations.PostgresMigrations
{
    public partial class AddInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("f4416227-0a1c-4b49-95d6-809018de1d13"), new DateTime(2021, 6, 1, 22, 24, 15, 222, DateTimeKind.Utc).AddTicks(7044), "Owner", new DateTime(2021, 6, 1, 22, 24, 15, 222, DateTimeKind.Utc).AddTicks(7044) },
                    { new Guid("f3f2203a-4002-418e-bcc4-a6e28e538534"), new DateTime(2021, 6, 1, 22, 24, 15, 222, DateTimeKind.Utc).AddTicks(7044), "Admin", new DateTime(2021, 6, 1, 22, 24, 15, 222, DateTimeKind.Utc).AddTicks(7044) },
                    { new Guid("08c07505-ee0f-438d-9579-4f158b11c9cb"), new DateTime(2021, 6, 1, 22, 24, 15, 222, DateTimeKind.Utc).AddTicks(7044), "Moderator", new DateTime(2021, 6, 1, 22, 24, 15, 222, DateTimeKind.Utc).AddTicks(7044) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Mail", "Password", "RoleId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("b12c7f71-a0e8-44ec-9a68-6759c02ca643"), new DateTime(2021, 6, 1, 22, 24, 15, 222, DateTimeKind.Utc).AddTicks(7044), "Owner", "AQAAAAEAACcQAAAAENAnCVyWq0lo9yySX3Ka7WMkN6jmIjUBKz1CohwrKt5ngJpr5Pq4fY4sLSXWs3ul/A==", new Guid("f4416227-0a1c-4b49-95d6-809018de1d13"), new DateTime(2021, 6, 1, 22, 24, 15, 222, DateTimeKind.Utc).AddTicks(7044) },
                    { new Guid("bf39c08e-b8fa-477e-82dc-502cc9c4f96c"), new DateTime(2021, 6, 1, 22, 24, 15, 222, DateTimeKind.Utc).AddTicks(7044), "Admin", "AQAAAAEAACcQAAAAENAnCVyWq0lo9yySX3Ka7WMkN6jmIjUBKz1CohwrKt5ngJpr5Pq4fY4sLSXWs3ul/A==", new Guid("f3f2203a-4002-418e-bcc4-a6e28e538534"), new DateTime(2021, 6, 1, 22, 24, 15, 222, DateTimeKind.Utc).AddTicks(7044) },
                    { new Guid("3df2af1a-b9e2-4299-bdc4-7ba49e039b2f"), new DateTime(2021, 6, 1, 22, 24, 15, 222, DateTimeKind.Utc).AddTicks(7044), "Moderator", "AQAAAAEAACcQAAAAENAnCVyWq0lo9yySX3Ka7WMkN6jmIjUBKz1CohwrKt5ngJpr5Pq4fY4sLSXWs3ul/A==", new Guid("08c07505-ee0f-438d-9579-4f158b11c9cb"), new DateTime(2021, 6, 1, 22, 24, 15, 222, DateTimeKind.Utc).AddTicks(7044) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3df2af1a-b9e2-4299-bdc4-7ba49e039b2f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b12c7f71-a0e8-44ec-9a68-6759c02ca643"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bf39c08e-b8fa-477e-82dc-502cc9c4f96c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("08c07505-ee0f-438d-9579-4f158b11c9cb"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f3f2203a-4002-418e-bcc4-a6e28e538534"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f4416227-0a1c-4b49-95d6-809018de1d13"));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "character varying(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);
        }
    }
}
