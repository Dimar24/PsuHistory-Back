using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PsuHistory.Data.EF.SQL.Migrations.PostgresMigrations
{
    public partial class AddNewField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Burials",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("1228a08d-9596-4b86-9125-f42833590414"), new DateTime(2021, 6, 7, 19, 11, 1, 560, DateTimeKind.Utc).AddTicks(8673), "Owner", new DateTime(2021, 6, 7, 19, 11, 1, 560, DateTimeKind.Utc).AddTicks(8673) },
                    { new Guid("ddb44385-fa68-489e-bd46-f30f6ffe5786"), new DateTime(2021, 6, 7, 19, 11, 1, 560, DateTimeKind.Utc).AddTicks(8673), "Admin", new DateTime(2021, 6, 7, 19, 11, 1, 560, DateTimeKind.Utc).AddTicks(8673) },
                    { new Guid("68611f87-f6bb-4490-b2c3-a7ab3b502823"), new DateTime(2021, 6, 7, 19, 11, 1, 560, DateTimeKind.Utc).AddTicks(8673), "Moderator", new DateTime(2021, 6, 7, 19, 11, 1, 560, DateTimeKind.Utc).AddTicks(8673) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Mail", "Password", "RoleId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("2017b538-ac76-4633-baf8-80f0bec88856"), new DateTime(2021, 6, 7, 19, 11, 1, 560, DateTimeKind.Utc).AddTicks(8673), "Owner", "AQAAAAEAACcQAAAAENAnCVyWq0lo9yySX3Ka7WMkN6jmIjUBKz1CohwrKt5ngJpr5Pq4fY4sLSXWs3ul/A==", new Guid("1228a08d-9596-4b86-9125-f42833590414"), new DateTime(2021, 6, 7, 19, 11, 1, 560, DateTimeKind.Utc).AddTicks(8673) },
                    { new Guid("2e93ac0c-3053-4fc1-ad54-7b1dff9b1f5b"), new DateTime(2021, 6, 7, 19, 11, 1, 560, DateTimeKind.Utc).AddTicks(8673), "Admin", "AQAAAAEAACcQAAAAENAnCVyWq0lo9yySX3Ka7WMkN6jmIjUBKz1CohwrKt5ngJpr5Pq4fY4sLSXWs3ul/A==", new Guid("ddb44385-fa68-489e-bd46-f30f6ffe5786"), new DateTime(2021, 6, 7, 19, 11, 1, 560, DateTimeKind.Utc).AddTicks(8673) },
                    { new Guid("d42aef05-d907-4cb2-9401-5800513f8a54"), new DateTime(2021, 6, 7, 19, 11, 1, 560, DateTimeKind.Utc).AddTicks(8673), "Moderator", "AQAAAAEAACcQAAAAENAnCVyWq0lo9yySX3Ka7WMkN6jmIjUBKz1CohwrKt5ngJpr5Pq4fY4sLSXWs3ul/A==", new Guid("68611f87-f6bb-4490-b2c3-a7ab3b502823"), new DateTime(2021, 6, 7, 19, 11, 1, 560, DateTimeKind.Utc).AddTicks(8673) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2017b538-ac76-4633-baf8-80f0bec88856"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2e93ac0c-3053-4fc1-ad54-7b1dff9b1f5b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d42aef05-d907-4cb2-9401-5800513f8a54"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1228a08d-9596-4b86-9125-f42833590414"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("68611f87-f6bb-4490-b2c3-a7ab3b502823"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ddb44385-fa68-489e-bd46-f30f6ffe5786"));

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Burials");

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
    }
}
