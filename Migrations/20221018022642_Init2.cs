using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoWebTemplate.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recieves_Carts_CartId",
                table: "Recieves");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "Recieves",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "Phone",
                table: "Recieves",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Recieves_Carts_CartId",
                table: "Recieves",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recieves_Carts_CartId",
                table: "Recieves");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Recieves");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "Recieves",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recieves_Carts_CartId",
                table: "Recieves",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
