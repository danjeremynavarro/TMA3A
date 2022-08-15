using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMA3A.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "ItemCategoryId",
                table: "Product",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ItemCategoryId",
                table: "Product",
                column: "ItemCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ItemCategory_ItemCategoryId",
                table: "Product",
                column: "ItemCategoryId",
                principalTable: "ItemCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ItemCategory_ItemCategoryId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ItemCategoryId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ItemCategoryId",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
