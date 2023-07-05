using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentApp.Core.Entities;

namespace StudentApp.Api.EntityConfiguration
{
	public class UserEntityConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasIndex(u => u.Email).IsUnique().HasName("IX_UniqueEmail");
			builder.HasKey(s => s.Id);
			builder.Property(s => s.Id).ValueGeneratedOnAdd().HasColumnName("id").HasColumnOrder(0);
			builder.Property(u => u.FirstName).IsRequired().HasColumnName("first_name").HasColumnOrder(1);
			builder.Property(u => u.LastName).IsRequired().HasColumnName("last_name").HasColumnOrder(2);
			builder.Property(u => u.Email).IsRequired().HasColumnName("email").HasColumnOrder(3);
			builder.Property(u => u.Password).IsRequired().HasColumnName("password").HasColumnOrder(4);
			builder.Property(u => u.RoleId).HasColumnName("role_id").HasColumnOrder(5);
			builder.Property(s => s.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("GETDATE()").HasColumnOrder(6);
			builder.Property(s => s.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate().HasColumnOrder(7);

			// Soft delete configuration
			builder.Property(r => r.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false).HasColumnOrder(8);
			builder.Property(s => s.DeletedAt).HasColumnName("deleted_at").HasDefaultValueSql(null).HasColumnOrder(9);
		}
	}
}