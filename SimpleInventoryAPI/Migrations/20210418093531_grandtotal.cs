using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleInventoryAPI.Migrations
{
    public partial class grandtotal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "info",
                table: "SelectTwoModels",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GrandTotal",
                table: "PurchaseOrders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "PO",
                columns: table => new
                {
                    RowNo = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    PurchaseOrderNumber = table.Column<string>(nullable: true),
                    Supplier = table.Column<string>(nullable: true),
                    IsDraft = table.Column<bool>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    SubTotal = table.Column<decimal>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false),
                    Additional = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "POItems",
                columns: table => new
                {
                    RowNo = table.Column<int>(nullable: false),
                    PurchaseOrderId = table.Column<int>(nullable: false),
                    PartNumber = table.Column<string>(nullable: true),
                    PartDesc = table.Column<string>(nullable: true),
                    Qty = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PO");

            migrationBuilder.DropTable(
                name: "POItems");

            migrationBuilder.DropColumn(
                name: "info",
                table: "SelectTwoModels");

            migrationBuilder.DropColumn(
                name: "GrandTotal",
                table: "PurchaseOrders");
        }
    }
}
