using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentApp.Entity;

namespace StudentApp.EntityConfiguration
{
	public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.HasKey(r => r.Id);
			builder.Property(r => r.Id).ValueGeneratedOnAdd().HasColumnName("id").HasColumnOrder(0);
			builder.Property(r => r.Name).HasColumnName("name").IsRequired().HasColumnOrder(1);

			builder.Property(s => s.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("GETDATE()").HasColumnOrder(2);
			builder.Property(s => s.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate().HasColumnOrder(3);

			// Soft delete configuration
			builder.Property(r => r.IsDeleted).HasDefaultValue(false).HasColumnOrder(4);
			builder.Property(s => s.DeletedAt).HasColumnName("deleted_at").HasDefaultValue(null).HasColumnOrder(5);


			builder.HasMany(u => u.users).WithOne(r => r.role).HasForeignKey(u => u.RoleId).OnDelete(DeleteBehavior.Cascade);
		}

	}
}

