using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PsuHistory.Data.EF.SQL.Migrations.MsSqlMigrations
{
    public partial class AddNewField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Burials",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new Guid("8bbdc4ba-31c7-4300-aa5e-12e366ecf9e9"), new DateTime(2021, 6, 7, 19, 10, 6, 36, DateTimeKind.Utc).AddTicks(9998), "Owner", new DateTime(2021, 6, 7, 19, 10, 6, 36, DateTimeKind.Utc).AddTicks(9998) });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new Guid("439b48bf-b0f1-41c1-bd5c-048b3e606738"), new DateTime(2021, 6, 7, 19, 10, 6, 36, DateTimeKind.Utc).AddTicks(9998), "Admin", new DateTime(2021, 6, 7, 19, 10, 6, 36, DateTimeKind.Utc).AddTicks(9998) });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new Guid("3b72f5f3-1207-4a25-9047-212170f3dac5"), new DateTime(2021, 6, 7, 19, 10, 6, 36, DateTimeKind.Utc).AddTicks(9998), "Moderator", new DateTime(2021, 6, 7, 19, 10, 6, 36, DateTimeKind.Utc).AddTicks(9998) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Mail", "Password", "RoleId", "UpdatedAt" },
                values: new object[] { new Guid("40da2ff4-4290-4c09-ae65-c396b9de63e8"), new DateTime(2021, 6, 7, 19, 10, 6, 36, DateTimeKind.Utc).AddTicks(9998), "Owner", "AQAAAAEAACcQAAAAENAnCVyWq0lo9yySX3Ka7WMkN6jmIjUBKz1CohwrKt5ngJpr5Pq4fY4sLSXWs3ul/A==", new Guid("8bbdc4ba-31c7-4300-aa5e-12e366ecf9e9"), new DateTime(2021, 6, 7, 19, 10, 6, 36, DateTimeKind.Utc).AddTicks(9998) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Mail", "Password", "RoleId", "UpdatedAt" },
                values: new object[] { new Guid("df406afd-c942-489a-8572-f33d53bcad2a"), new DateTime(2021, 6, 7, 19, 10, 6, 36, DateTimeKind.Utc).AddTicks(9998), "Admin", "AQAAAAEAACcQAAAAENAnCVyWq0lo9yySX3Ka7WMkN6jmIjUBKz1CohwrKt5ngJpr5Pq4fY4sLSXWs3ul/A==", new Guid("439b48bf-b0f1-41c1-bd5c-048b3e606738"), new DateTime(2021, 6, 7, 19, 10, 6, 36, DateTimeKind.Utc).AddTicks(9998) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Mail", "Password", "RoleId", "UpdatedAt" },
                values: new object[] { new Guid("60fd564b-7052-46f1-a21c-a2d82202013f"), new DateTime(2021, 6, 7, 19, 10, 6, 36, DateTimeKind.Utc).AddTicks(9998), "Moderator", "AQAAAAEAACcQAAAAENAnCVyWq0lo9yySX3Ka7WMkN6jmIjUBKz1CohwrKt5ngJpr5Pq4fY4sLSXWs3ul/A==", new Guid("3b72f5f3-1207-4a25-9047-212170f3dac5"), new DateTime(2021, 6, 7, 19, 10, 6, 36, DateTimeKind.Utc).AddTicks(9998) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("40da2ff4-4290-4c09-ae65-c396b9de63e8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("60fd564b-7052-46f1-a21c-a2d82202013f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("df406afd-c942-489a-8572-f33d53bcad2a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3b72f5f3-1207-4a25-9047-212170f3dac5"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("439b48bf-b0f1-41c1-bd5c-048b3e606738"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8bbdc4ba-31c7-4300-aa5e-12e366ecf9e9"));

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Burials");

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
    }
}
