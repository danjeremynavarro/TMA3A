using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMA3A.Migrations
{
    public partial class AllowNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ItemCategory_ItemCategoryId",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "ItemCategoryId",
                table: "Product",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ItemCategory_ItemCategoryId",
                table: "Product",
                column: "ItemCategoryId",
                principalTable: "ItemCategory",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ItemCategory_ItemCategoryId",
                table: "Product");

            migrationBuilder.AlterColumn<string>(
                name: "ItemCategoryId",
                table: "Product",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ItemCategory_ItemCategoryId",
                table: "Product",
                column: "ItemCategoryId",
                principalTable: "ItemCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
