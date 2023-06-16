using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentApp.Migrations
{
	/// <inheritdoc />
	public partial class AddStudentApp : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "roles",
				columns: table => new {
					id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
					deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_roles", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "users",
				columns: table => new {
					id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					email = table.Column<string>(type: "nvarchar(450)", nullable: false),
					password = table.Column<string>(type: "nvarchar(max)", nullable: false),
					role_id = table.Column<int>(type: "int", nullable: false),
					created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
					deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_users", x => x.id);
					table.ForeignKey(
						name: "FK_users_roles_role_id",
						column: x => x.role_id,
						principalTable: "roles",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "students",
				columns: table => new {
					id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					user_id = table.Column<int>(type: "int", nullable: false),
					created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
					deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_students", x => x.id);
					table.ForeignKey(
						name: "FK_students_users_user_id",
						column: x => x.user_id,
						principalTable: "users",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "teachers",
				columns: table => new {
					id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					user_id = table.Column<int>(type: "int", nullable: false),
					created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
					deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_teachers", x => x.id);
					table.ForeignKey(
						name: "FK_teachers_users_user_id",
						column: x => x.user_id,
						principalTable: "users",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "student_teacher",
				columns: table => new {
					id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					student_id = table.Column<int>(type: "int", nullable: false),
					techer_id = table.Column<int>(type: "int", nullable: false),
					created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					is_deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
					deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_student_teacher", x => x.id);
					table.ForeignKey(
						name: "FK_student_teacher_students_student_id",
						column: x => x.student_id,
						principalTable: "students",
						principalColumn: "id");
					table.ForeignKey(
						name: "FK_student_teacher_teachers_techer_id",
						column: x => x.techer_id,
						principalTable: "teachers",
						principalColumn: "id");
				});

			migrationBuilder.InsertData(
				table: "roles",
				columns: new[] { "id", "created_at", "deleted_at", "name" },
				values: new object[,]
				{
					{ 1, new DateTime(2023, 6, 15, 12, 21, 47, 17, DateTimeKind.Local).AddTicks(1931), null, "Student" },
					{ 2, new DateTime(2023, 6, 15, 12, 21, 47, 17, DateTimeKind.Local).AddTicks(1943), null, "Teacher" }
				});

			migrationBuilder.CreateIndex(
				name: "IX_student_teacher_student_id",
				table: "student_teacher",
				column: "student_id");

			migrationBuilder.CreateIndex(
				name: "IX_student_teacher_techer_id",
				table: "student_teacher",
				column: "techer_id");

			migrationBuilder.CreateIndex(
				name: "IX_students_user_id",
				table: "students",
				column: "user_id",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_teachers_user_id",
				table: "teachers",
				column: "user_id",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_UniqueEmail",
				table: "users",
				column: "email",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_users_role_id",
				table: "users",
				column: "role_id");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "student_teacher");

			migrationBuilder.DropTable(
				name: "students");

			migrationBuilder.DropTable(
				name: "teachers");

			migrationBuilder.DropTable(
				name: "users");

			migrationBuilder.DropTable(
				name: "roles");
		}
	}
}
