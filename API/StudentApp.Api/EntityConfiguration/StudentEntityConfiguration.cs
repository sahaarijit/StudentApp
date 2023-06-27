using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentApp.Entity;

namespace StudentApp.EntityConfiguration
{
	public class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
	{
		public void Configure(EntityTypeBuilder<Student> builder)
		{
			#region Properties
			builder.HasKey(s => s.Id);
			builder.Property(s => s.Id).ValueGeneratedOnAdd().HasColumnName("id").HasColumnOrder(0);
			builder.Property(s => s.UserId).HasColumnName("user_id").HasColumnOrder(1);
			builder.Property(s => s.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("GETDATE()").HasColumnOrder(2);
			builder.Property(s => s.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate().HasColumnOrder(3);
			#endregion

			#region Soft delete
			builder.Property(r => r.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).HasColumnOrder(4);
			builder.Property(s => s.DeletedAt).HasColumnName("deleted_at").HasDefaultValueSql(null).HasColumnOrder(5);
			#endregion

			#region Relations
			builder.HasOne(s => s.user).WithOne(ad => ad.student).OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(u => u.Students).WithOne(r => r.Student).HasForeignKey(u => u.StudentId).OnDelete(DeleteBehavior.NoAction);
			#endregion
		}
	}
}
