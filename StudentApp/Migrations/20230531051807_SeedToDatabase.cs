using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SampleProject.Migrations
{
	/// <inheritdoc />
	public partial class SeedToDatabase : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.InsertData(
				table: "Roles",
				columns: new[] { "id", "CreatedAt", "DeletedAt", "IsDeleted", "name" },
				values: new object[,]
				{
					{ 1, new DateTime(2023, 5, 31, 10, 48, 7, 565, DateTimeKind.Local).AddTicks(6245), null, false, "Student" },
					{ 2, new DateTime(2023, 5, 31, 10, 48, 7, 565, DateTimeKind.Local).AddTicks(6259), null, false, "Teacher" }
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "Roles",
				keyColumn: "id",
				keyValue: 1);

			migrationBuilder.DeleteData(
				table: "Roles",
				keyColumn: "id",
				keyValue: 2);
		}
	}
}
