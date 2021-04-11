using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleInventoryAPI.Migrations
{
    public partial class addednotesandtotaltocogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "ProductComponents",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "ProductComponents",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "ProductComponents");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "ProductComponents");
        }
    }
}
