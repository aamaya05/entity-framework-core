using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace proyectEF.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "category",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "category",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "category",
                columns: new[] { "id", "description", "name", "Point", "Uuid" },
                values: new object[,]
                {
                    { 1, null, "Actividades Pendientes", 20, new Guid("9cfab1c9-c234-42e4-a64f-78be3eb8e12d") },
                    { 2, null, "Actividades Personales", 20, new Guid("9cfab1c9-c234-42e4-a64f-78be3eb8e56d") }
                });

            migrationBuilder.InsertData(
                table: "todo",
                columns: new[] { "id", "CategoryId", "description", "title", "uuid" },
                values: new object[,]
                {
                    { 1, 1, null, "Pago de servicios publicos", new Guid("34d722eb-5fda-4e1f-b838-150140ba4e30") },
                    { 2, 2, null, "Pago de cuota", new Guid("34d722eb-5fda-4e1f-b838-150140ba4e11") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "todo",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "todo",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "category",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "id",
                table: "category",
                newName: "CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "category",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
