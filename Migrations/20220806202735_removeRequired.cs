using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMA3A.Migrations
{
    public partial class removeRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_UserUsername",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "UserUsername",
                table: "Order",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_UserUsername",
                table: "Order",
                column: "UserUsername",
                principalTable: "User",
                principalColumn: "Username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_User_UserUsername",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "UserUsername",
                table: "Order",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_User_UserUsername",
                table: "Order",
                column: "UserUsername",
                principalTable: "User",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
