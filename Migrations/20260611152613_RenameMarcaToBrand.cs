using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMS_Backend.Migrations
{
    /// <inheritdoc />
    public partial class RenameMarcaToBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Marca",
                table: "Products",
                newName: "Brand");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "Products",
                newName: "Marca");
        }
    }
}
