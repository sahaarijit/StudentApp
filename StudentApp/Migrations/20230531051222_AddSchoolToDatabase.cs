using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentApp.Migrations
{
	/// <inheritdoc />
	public partial class AddSchoolToDatabase : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Roles",
				columns: table => new {
					id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false),
					DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_Roles", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new {
					id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					RoleId = table.Column<int>(type: "int", nullable: false),
					email = table.Column<string>(type: "nvarchar(max)", nullable: false),
					password = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false),
					DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_Users", x => x.id);
					table.ForeignKey(
						name: "FK_Users_Roles_RoleId",
						column: x => x.RoleId,
						principalTable: "Roles",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "StudentTeacher",
				columns: table => new {
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					StudentId = table.Column<int>(type: "int", nullable: false),
					TeacherId = table.Column<int>(type: "int", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false),
					DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_StudentTeacher", x => x.Id);
					table.ForeignKey(
						name: "FK_StudentTeacher_Users_StudentId",
						column: x => x.StudentId,
						principalTable: "Users",
						principalColumn: "id",
						onDelete: ReferentialAction.NoAction);
					table.ForeignKey(
						name: "FK_StudentTeacher_Users_TeacherId",
						column: x => x.TeacherId,
						principalTable: "Users",
						principalColumn: "id",
						onDelete: ReferentialAction.NoAction);
				});

			migrationBuilder.CreateIndex(
				name: "IX_StudentTeacher_StudentId",
				table: "StudentTeacher",
				column: "StudentId");

			migrationBuilder.CreateIndex(
				name: "IX_StudentTeacher_TeacherId",
				table: "StudentTeacher",
				column: "TeacherId");

			migrationBuilder.CreateIndex(
				name: "IX_Users_RoleId",
				table: "Users",
				column: "RoleId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "StudentTeacher");

			migrationBuilder.DropTable(
				name: "Users");

			migrationBuilder.DropTable(
				name: "Roles");
		}
	}
}
