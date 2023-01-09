using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyectEF.Migrations
{
    /// <inheritdoc />
    public partial class AddPointColumnOnCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Uuid",
                table: "todo",
                newName: "uuid");

            migrationBuilder.AddColumn<int>(
                name: "Point",
                table: "category",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Point",
                table: "category");

            migrationBuilder.RenameColumn(
                name: "uuid",
                table: "todo",
                newName: "Uuid");
        }
    }
}
