using Microsoft.EntityFrameworkCore;
using StudentApp.Entity;
using StudentApp.EntityConfiguration;

namespace StudentApp.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<Role> Roles { get; set; }
		public DbSet<User> Users { get; set; }

		public DbSet<Student> Students { get; set; }

		public DbSet<Teacher> Teachers { get; set; }

		public DbSet<StudentTeacher> StudentTeacher { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//// Soft delete configuration
			//modelBuilder.Entity<BaseEntity>().HasQueryFilter(x => !x.IsDeleted);

			////UpdatedAt configuration
			//modelBuilder.Entity<BaseEntity>()
			//	  .Property(s => s.UpdatedAt)
			//	  .HasDefaultValueSql("GETDATE()");

			//modelBuilder.Entity<Role>().HasMany(u => u.users).WithOne(r => r.role).HasForeignKey(u => u.RoleId).OnDelete(DeleteBehavior.Cascade);
			//modelBuilder.Entity<Student>().HasOne(s => s.user).WithOne(ad => ad.student).OnDelete(DeleteBehavior.Cascade);

			//modelBuilder.Entity<Student>().HasMany(u => u.Students).WithOne(r => r.Student).HasForeignKey(u => u.StudentId).OnDelete(DeleteBehavior.NoAction);

			//modelBuilder.Entity<Teacher>().HasOne(s => s.user).WithOne(ad => ad.teacher).OnDelete(DeleteBehavior.Cascade);

			//modelBuilder.Entity<Teacher>().HasMany(u => u.Teachers).WithOne(r => r.Teacher).HasForeignKey(u => u.TeacherId).OnDelete(DeleteBehavior.NoAction);

			modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());

			modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

			modelBuilder.ApplyConfiguration(new StudentEntityConfiguration());

			modelBuilder.ApplyConfiguration(new TeacherEntityConfiguration());




			//Masterdata or seed data configuration
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
		}
	}
}

