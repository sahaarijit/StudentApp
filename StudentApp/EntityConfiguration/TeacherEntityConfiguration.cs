using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentApp.Entity;


namespace StudentApp.EntityConfiguration
{
	public class TeacherEntityConfiguration : IEntityTypeConfiguration<Teacher>
	{
		public void Configure(EntityTypeBuilder<Teacher> builder)
		{
			builder.HasKey(s => s.Id);
			builder.Property(s => s.Id).ValueGeneratedOnAdd().HasColumnName("id").HasColumnOrder(0);
			builder.Property(t => t.TeacherId).HasColumnName("teacher_id").HasColumnOrder(1);
			builder.Property(s => s.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("GETDATE()").HasColumnOrder(2);
			builder.Property(s => s.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate().HasColumnOrder(3);

			// Soft delete configuration
			builder.Property(r => r.IsDeleted).HasDefaultValue(false).HasColumnOrder(4);
			builder.Property(s => s.DeletedAt).HasColumnName("deleted_at").HasDefaultValue(null).HasColumnOrder(5);

			builder.HasOne(s => s.user)
				.WithOne(ad => ad.teacher).OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(u => u.Teachers).WithOne(r => r.Teacher).HasForeignKey(u => u.TeacherId).OnDelete(DeleteBehavior.NoAction);
		}
	}
}
