using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductStore.Migrations
{
    /// <inheritdoc />
    public partial class AddingFKback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_CatagorieId",
                table: "Products",
                column: "CatagorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categorys_CatagorieId",
                table: "Products",
                column: "CatagorieId",
                principalTable: "Categorys",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categorys_CatagorieId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CatagorieId",
                table: "Products");
        }
    }
}
