using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentApp.Core.Entities;

namespace StudentApp.Api.EntityConfiguration
{
	public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			#region Properties
			builder.HasKey(r => r.Id);
			builder.Property(r => r.Id).ValueGeneratedOnAdd().HasColumnName("id").HasColumnOrder(0);
			builder.Property(r => r.Name).HasColumnName("name").IsRequired().HasColumnOrder(1);
			builder.Property(s => s.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("GETDATE()").HasColumnOrder(2);
			builder.Property(s => s.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate().HasColumnOrder(3);
			#endregion

			#region Soft delete
			builder.Property(r => r.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).HasColumnOrder(4);
			builder.Property(s => s.DeletedAt).HasColumnName("deleted_at").HasDefaultValue(null).HasColumnOrder(5);
			#endregion

			#region Relations
			builder.HasMany(u => u.users).WithOne(r => r.role).HasForeignKey(u => u.RoleId).OnDelete(DeleteBehavior.Cascade);
			#endregion
		}

	}
}
