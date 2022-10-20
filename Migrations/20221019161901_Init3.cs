using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoWebTemplate.Migrations
{
    public partial class Init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recieves_Carts_CartId",
                table: "Recieves");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "Recieves",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Recieves_CartId",
                table: "Recieves",
                newName: "IX_Recieves_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recieves_Products_ProductId",
                table: "Recieves",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recieves_Products_ProductId",
                table: "Recieves");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Recieves",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_Recieves_ProductId",
                table: "Recieves",
                newName: "IX_Recieves_CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recieves_Carts_CartId",
                table: "Recieves",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }
    }
}
