using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PsuHistory.Data.EF.SQL.Migrations.MsSqlMigrations
{
    public partial class AddInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new Guid("d3b96c5c-d7b7-4e92-b8a3-0ff9d73f9004"), new DateTime(2021, 6, 1, 22, 23, 48, 50, DateTimeKind.Utc).AddTicks(9914), "Owner", new DateTime(2021, 6, 1, 22, 23, 48, 50, DateTimeKind.Utc).AddTicks(9914) });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new Guid("10a950d0-6580-4009-bff4-e5adc74ce129"), new DateTime(2021, 6, 1, 22, 23, 48, 50, DateTimeKind.Utc).AddTicks(9914), "Admin", new DateTime(2021, 6, 1, 22, 23, 48, 50, DateTimeKind.Utc).AddTicks(9914) });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new Guid("3dd5da3c-857c-4c68-8088-fbaca917f878"), new DateTime(2021, 6, 1, 22, 23, 48, 50, DateTimeKind.Utc).AddTicks(9914), "Moderator", new DateTime(2021, 6, 1, 22, 23, 48, 50, DateTimeKind.Utc).AddTicks(9914) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Mail", "Password", "RoleId", "UpdatedAt" },
                values: new object[] { new Guid("47e3201b-23a6-4c35-8fdf-ef13626f68a7"), new DateTime(2021, 6, 1, 22, 23, 48, 50, DateTimeKind.Utc).AddTicks(9914), "Owner", "AQAAAAEAACcQAAAAENAnCVyWq0lo9yySX3Ka7WMkN6jmIjUBKz1CohwrKt5ngJpr5Pq4fY4sLSXWs3ul/A==", new Guid("d3b96c5c-d7b7-4e92-b8a3-0ff9d73f9004"), new DateTime(2021, 6, 1, 22, 23, 48, 50, DateTimeKind.Utc).AddTicks(9914) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Mail", "Password", "RoleId", "UpdatedAt" },
                values: new object[] { new Guid("42f0f55d-a7c9-4284-a149-18da1dcc997d"), new DateTime(2021, 6, 1, 22, 23, 48, 50, DateTimeKind.Utc).AddTicks(9914), "Admin", "AQAAAAEAACcQAAAAENAnCVyWq0lo9yySX3Ka7WMkN6jmIjUBKz1CohwrKt5ngJpr5Pq4fY4sLSXWs3ul/A==", new Guid("10a950d0-6580-4009-bff4-e5adc74ce129"), new DateTime(2021, 6, 1, 22, 23, 48, 50, DateTimeKind.Utc).AddTicks(9914) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Mail", "Password", "RoleId", "UpdatedAt" },
                values: new object[] { new Guid("f2bfbe83-7d94-4687-bcea-dc48d3ee286f"), new DateTime(2021, 6, 1, 22, 23, 48, 50, DateTimeKind.Utc).AddTicks(9914), "Moderator", "AQAAAAEAACcQAAAAENAnCVyWq0lo9yySX3Ka7WMkN6jmIjUBKz1CohwrKt5ngJpr5Pq4fY4sLSXWs3ul/A==", new Guid("3dd5da3c-857c-4c68-8088-fbaca917f878"), new DateTime(2021, 6, 1, 22, 23, 48, 50, DateTimeKind.Utc).AddTicks(9914) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("42f0f55d-a7c9-4284-a149-18da1dcc997d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("47e3201b-23a6-4c35-8fdf-ef13626f68a7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f2bfbe83-7d94-4687-bcea-dc48d3ee286f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("10a950d0-6580-4009-bff4-e5adc74ce129"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3dd5da3c-857c-4c68-8088-fbaca917f878"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d3b96c5c-d7b7-4e92-b8a3-0ff9d73f9004"));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);
        }
    }
}
