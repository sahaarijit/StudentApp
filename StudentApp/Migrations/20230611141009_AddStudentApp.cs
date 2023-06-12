using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
					IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
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
					IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
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
					student_id = table.Column<int>(type: "int", nullable: false),
					created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
					deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_students", x => x.id);
					table.ForeignKey(
						name: "FK_students_users_student_id",
						column: x => x.student_id,
						principalTable: "users",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "teachers",
				columns: table => new {
					id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					teacher_id = table.Column<int>(type: "int", nullable: false),
					created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
					deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_teachers", x => x.id);
					table.ForeignKey(
						name: "FK_teachers_users_teacher_id",
						column: x => x.teacher_id,
						principalTable: "users",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "student_teacher",
				columns: table => new {
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					StudentId = table.Column<int>(type: "int", nullable: false),
					TeacherId = table.Column<int>(type: "int", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					IsDeleted = table.Column<bool>(type: "bit", nullable: false),
					DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table => {
					table.PrimaryKey("PK_student_teacher", x => x.Id);
					table.ForeignKey(
						name: "FK_student_teacher_students_StudentId",
						column: x => x.StudentId,
						principalTable: "students",
						principalColumn: "id");
					table.ForeignKey(
						name: "FK_student_teacher_teachers_TeacherId",
						column: x => x.TeacherId,
						principalTable: "teachers",
						principalColumn: "id");
				});

			migrationBuilder.CreateIndex(
				name: "IX_student_teacher_StudentId",
				table: "student_teacher",
				column: "StudentId");

			migrationBuilder.CreateIndex(
				name: "IX_student_teacher_TeacherId",
				table: "student_teacher",
				column: "TeacherId");

			migrationBuilder.CreateIndex(
				name: "IX_students_student_id",
				table: "students",
				column: "student_id",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_teachers_teacher_id",
				table: "teachers",
				column: "teacher_id",
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
