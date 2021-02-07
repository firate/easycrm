using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyCRM.Data.Migrations
{
    public partial class Address_Date_Info_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Addresses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Addresses",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 2, 3, 23, 5, 28, 989, DateTimeKind.Local).AddTicks(8118), new DateTime(2021, 2, 3, 23, 5, 28, 990, DateTimeKind.Local).AddTicks(6939) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Addresses");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 2, 2, 23, 56, 20, 417, DateTimeKind.Local).AddTicks(4070), new DateTime(2021, 2, 2, 23, 56, 20, 418, DateTimeKind.Local).AddTicks(2579) });
        }
    }
}
