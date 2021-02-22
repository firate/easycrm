using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyCRM.Data.Migrations
{
    public partial class SalesOrder_Date_Info_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "SalesOrders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "SalesOrders",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 2, 22, 22, 47, 56, 532, DateTimeKind.Local).AddTicks(3360), new DateTime(2021, 2, 22, 22, 47, 56, 533, DateTimeKind.Local).AddTicks(1539) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "SalesOrders");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "SalesOrders");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "AccountID",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2021, 2, 3, 23, 5, 28, 989, DateTimeKind.Local).AddTicks(8118), new DateTime(2021, 2, 3, 23, 5, 28, 990, DateTimeKind.Local).AddTicks(6939) });
        }
    }
}
