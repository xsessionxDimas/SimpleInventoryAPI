using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleInventoryAPI.Migrations
{
    public partial class fixpurchaseorderitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItem_ComponentId",
                table: "PurchaseOrderItem",
                column: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderItem_Components_ComponentId",
                table: "PurchaseOrderItem",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderItem_Components_ComponentId",
                table: "PurchaseOrderItem");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrderItem_ComponentId",
                table: "PurchaseOrderItem");
        }
    }
}
