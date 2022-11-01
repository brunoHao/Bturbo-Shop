using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoWebTemplate.Migrations
{
    public partial class Init5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecieveId",
                table: "RecieveDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RecieveDetails_RecieveId",
                table: "RecieveDetails",
                column: "RecieveId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecieveDetails_Recieves_RecieveId",
                table: "RecieveDetails",
                column: "RecieveId",
                principalTable: "Recieves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecieveDetails_Recieves_RecieveId",
                table: "RecieveDetails");

            migrationBuilder.DropIndex(
                name: "IX_RecieveDetails_RecieveId",
                table: "RecieveDetails");

            migrationBuilder.DropColumn(
                name: "RecieveId",
                table: "RecieveDetails");
        }
    }
}
