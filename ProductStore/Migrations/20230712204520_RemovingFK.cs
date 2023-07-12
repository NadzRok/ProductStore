using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductStore.Migrations
{
    /// <inheritdoc />
    public partial class RemovingFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categorys_CatagoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CatagoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CatagoryId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "CatagorieId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatagorieId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "CatagoryId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CatagoryId",
                table: "Products",
                column: "CatagoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categorys_CatagoryId",
                table: "Products",
                column: "CatagoryId",
                principalTable: "Categorys",
                principalColumn: "CategoryId");
        }
    }
}
