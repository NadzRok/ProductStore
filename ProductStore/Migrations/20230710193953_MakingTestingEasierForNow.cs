using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductStore.Migrations
{
    /// <inheritdoc />
    public partial class MakingTestingEasierForNow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categorys_CatagoryId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "CatagoryId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categorys_CatagoryId",
                table: "Products",
                column: "CatagoryId",
                principalTable: "Categorys",
                principalColumn: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categorys_CatagoryId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "CatagoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categorys_CatagoryId",
                table: "Products",
                column: "CatagoryId",
                principalTable: "Categorys",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
