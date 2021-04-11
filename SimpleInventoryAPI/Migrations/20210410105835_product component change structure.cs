using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleInventoryAPI.Migrations
{
    public partial class productcomponentchangestructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductComponents_Components_ComponentId",
                table: "ProductComponents");

            migrationBuilder.DropIndex(
                name: "IX_ProductComponents_ComponentId",
                table: "ProductComponents");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                table: "ProductComponents");

            migrationBuilder.DropColumn(
                name: "CostPerUnit",
                table: "ProductComponents");

            migrationBuilder.DropColumn(
                name: "FreightPerUnit",
                table: "ProductComponents");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "ProductComponents");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "ProductComponents");

            migrationBuilder.DropColumn(
                name: "Usage",
                table: "ProductComponents");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "PurchaseOrderItem",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PurchaseOrderItem",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "ProductComponents",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductComponentItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    HeaderId = table.Column<int>(nullable: false),
                    ComponentId = table.Column<int>(nullable: false),
                    Usage = table.Column<int>(nullable: false),
                    CostPerUnit = table.Column<decimal>(nullable: false),
                    FreightPerUnit = table.Column<decimal>(nullable: true),
                    Total = table.Column<decimal>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComponentItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductComponentItem_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductComponentItem_ProductComponents_HeaderId",
                        column: x => x.HeaderId,
                        principalTable: "ProductComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductComponentItem_ComponentId",
                table: "ProductComponentItem",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComponentItem_HeaderId",
                table: "ProductComponentItem",
                column: "HeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductComponentItem");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ProductComponents");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "PurchaseOrderItem",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "PurchaseOrderItem",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "ComponentId",
                table: "ProductComponents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "CostPerUnit",
                table: "ProductComponents",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FreightPerUnit",
                table: "ProductComponents",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "ProductComponents",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "ProductComponents",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Usage",
                table: "ProductComponents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductComponents_ComponentId",
                table: "ProductComponents",
                column: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComponents_Components_ComponentId",
                table: "ProductComponents",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
