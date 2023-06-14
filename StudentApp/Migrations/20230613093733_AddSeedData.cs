using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable


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
					{ 1, new DateTime(2023, 6, 13, 15, 7, 33, 294, DateTimeKind.Local).AddTicks(1716), null, "Student" },
					{ 2, new DateTime(2023, 6, 13, 15, 7, 33, 294, DateTimeKind.Local).AddTicks(1730), null, "Teacher" }
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
