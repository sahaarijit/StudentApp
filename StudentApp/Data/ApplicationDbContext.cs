using Microsoft.EntityFrameworkCore;
using StudentApp.Entity;
using StudentApp.EntityConfiguration;

namespace StudentApp.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

		public DbSet<Role> Roles { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Student> Students { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<StudentTeacher> StudentTeacher { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			#region Entity Configs
			modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
			modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
			modelBuilder.ApplyConfiguration(new StudentEntityConfiguration());
			modelBuilder.ApplyConfiguration(new TeacherEntityConfiguration());
			modelBuilder.ApplyConfiguration(new StudentTeacherEntityConfiguration());
			#endregion

			#region Seeder Config
			modelBuilder.Entity<Role>().HasData(
			  new Role {
				  Id = 1,
				  Name = "Student"
			  },
			  new Role {
				  Id = 2,
				  Name = "Teacher"
			  }
			);
			#endregion
		}
	}
}
