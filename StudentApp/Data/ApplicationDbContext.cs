using Microsoft.EntityFrameworkCore;
using StudentApp.Models;


namespace StudentApp.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<Role> Roles { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<StudentTeacher> StudentTeacher { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Soft delete configuration
			modelBuilder.Entity<Role>().HasQueryFilter(x => !x.IsDeleted);
			modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
			modelBuilder.Entity<StudentTeacher>().HasQueryFilter(x => !x.IsDeleted);

			//UpdatedAt configuration
			modelBuilder.Entity<Role>()
				  .Property(s => s.UpdatedAt)
				  .HasDefaultValueSql("GETDATE()");

			modelBuilder.Entity<User>()
				 .Property(s => s.UpdatedAt)
				 .HasDefaultValueSql("GETDATE()");

			modelBuilder.Entity<StudentTeacher>()
				 .Property(s => s.UpdatedAt)
				 .HasDefaultValueSql("GETDATE()");

			////Many-to-many relationships

			//modelBuilder.Entity<User>()
			//	.HasMany(p => p.id)
			//	.WithMany(p =>p.teachers)
			//	.Map(m => {
			//		m.MapLeftKey("StudentId");
			//		m.MapRightKey("TeacherId");
			//		m.ToTable("StudentTeacher");
			//	});


			//Masterdata or seed data configuration
			modelBuilder.Entity<Role>().HasData(
			  new Role {
				  id = 1,
				  name = "Student"
			  },
			  new Role {
				  id = 2,
				  name = "Teacher"
			  }
			);
		}
	}
}

