using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMS_Backend.Migrations
{
    /// <inheritdoc />
    public partial class NewArchitecture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_SubCategories_Categories_CategoryId",
                table: "SubCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubCategories",
                table: "SubCategories");

            migrationBuilder.RenameTable(
                name: "SubCategories",
                newName: "Subcategories");

            migrationBuilder.RenameIndex(
                name: "IX_SubCategories_CategoryId",
                table: "Subcategories",
                newName: "IX_Subcategories_CategoryId");

            migrationBuilder.RenameColumn(
                name: "SubCategoryId",
                table: "Products",
                newName: "SubcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products",
                newName: "IX_Products_SubcategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subcategories",
                table: "Subcategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Subcategories_SubcategoryId",
                table: "Products",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategories_Categories_CategoryId",
                table: "Subcategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Subcategories_SubcategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Subcategories_Categories_CategoryId",
                table: "Subcategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subcategories",
                table: "Subcategories");

            migrationBuilder.RenameTable(
                name: "Subcategories",
                newName: "SubCategories");

            migrationBuilder.RenameIndex(
                name: "IX_Subcategories_CategoryId",
                table: "SubCategories",
                newName: "IX_SubCategories_CategoryId");

            migrationBuilder.RenameColumn(
                name: "SubcategoryId",
                table: "Products",
                newName: "SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_SubcategoryId",
                table: "Products",
                newName: "IX_Products_SubCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubCategories",
                table: "SubCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategories_Categories_CategoryId",
                table: "SubCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
