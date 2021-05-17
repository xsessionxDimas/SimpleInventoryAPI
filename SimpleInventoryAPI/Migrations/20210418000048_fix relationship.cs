using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleInventoryAPI.Migrations
{
    public partial class fixrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBatchItemSummary_ProductComponents_HeaderId",
                table: "ProductBatchItemSummary");

            migrationBuilder.DropIndex(
                name: "IX_ProductBatchItemSummary_HeaderId",
                table: "ProductBatchItemSummary");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComponentItem_HeaderId",
                table: "ProductComponentItem",
                column: "HeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComponentItem_ProductComponents_HeaderId",
                table: "ProductComponentItem",
                column: "HeaderId",
                principalTable: "ProductComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductComponentItem_ProductComponents_HeaderId",
                table: "ProductComponentItem");

            migrationBuilder.DropIndex(
                name: "IX_ProductComponentItem_HeaderId",
                table: "ProductComponentItem");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBatchItemSummary_HeaderId",
                table: "ProductBatchItemSummary",
                column: "HeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBatchItemSummary_ProductComponents_HeaderId",
                table: "ProductBatchItemSummary",
                column: "HeaderId",
                principalTable: "ProductComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
