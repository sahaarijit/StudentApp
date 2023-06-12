using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentApp.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "created_at", "deleted_at", "name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 11, 19, 45, 14, 690, DateTimeKind.Local).AddTicks(6904), null, "Student" },
                    { 2, new DateTime(2023, 6, 11, 19, 45, 14, 690, DateTimeKind.Local).AddTicks(6920), null, "Teacher" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2);
        }
    }
}
