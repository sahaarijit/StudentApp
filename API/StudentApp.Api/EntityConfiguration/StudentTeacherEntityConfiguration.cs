using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentApp.Core.Entities;

namespace StudentApp.Api.EntityConfiguration
{
	public class StudentTeacherEntityConfiguration : IEntityTypeConfiguration<StudentTeacher>
	{
		public void Configure(EntityTypeBuilder<StudentTeacher> builder)
		{
			#region Properties
			builder.HasKey(s => s.Id);
			builder.Property(s => s.Id).ValueGeneratedOnAdd().HasColumnName("id").HasColumnOrder(0);
			builder.Property(s => s.StudentId).HasColumnName("student_id").HasColumnOrder(1);
			builder.Property(s => s.TeacherId).HasColumnName("techer_id").HasColumnOrder(2);
			builder.Property(s => s.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("GETDATE()").HasColumnOrder(3);
			builder.Property(s => s.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate().HasColumnOrder(4);
			#endregion

			#region Soft delete
			builder.Property(r => r.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).HasColumnOrder(5);
			builder.Property(s => s.DeletedAt).HasColumnName("deleted_at").HasDefaultValue(null).HasColumnOrder(6);
			#endregion
		}
	}
}

