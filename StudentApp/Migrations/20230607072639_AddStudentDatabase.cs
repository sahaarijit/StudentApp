using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentApp.Migrations
{
	/// <inheritdoc />
	public partial class AddStudentDatabase : Migration
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
					created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
					updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					is_deleted = table.Column<bool>(type: "bit", nullable: false),
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
					email = table.Column<string>(type: "nvarchar(max)", nullable: false),
					password = table.Column<string>(type: "nvarchar(max)", nullable: false),
					created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
					updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					is_deleted = table.Column<bool>(type: "bit", nullable: false),
					deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
					role_id = table.Column<int>(type: "int", nullable: false)
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
				name: "student_teacher",
				columns: table => new {
					id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
					updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
					is_deleted = table.Column<bool>(type: "bit", nullable: false),
					deleted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
					student_id = table.Column<int>(type: "int", nullable: false),
					teacher_id = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table => {
					table.PrimaryKey("PK_student_teacher", x => x.id);
					table.ForeignKey(
						name: "FK_student_teacher_users_student_id",
						column: x => x.student_id,
						principalTable: "users",
						principalColumn: "id",
						onDelete: ReferentialAction.NoAction);
					table.ForeignKey(
						name: "FK_student_teacher_users_teacher_id",
						column: x => x.teacher_id,
						principalTable: "users",
						principalColumn: "id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_student_teacher_student_id",
				table: "student_teacher",
				column: "student_id");

			migrationBuilder.CreateIndex(
				name: "IX_student_teacher_teacher_id",
				table: "student_teacher",
				column: "teacher_id");

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
				name: "users");

			migrationBuilder.DropTable(
				name: "roles");
		}
	}
}
