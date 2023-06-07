
using System.ComponentModel.DataAnnotations.Schema;
namespace StudentApp.Models
{
	public class BaseEntity
	{
		[Column("created_at", Order = 996)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		[Column("updated_at", Order = 997)]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedAt { get; set; } = DateTime.Now;

		[Column("is_deleted", Order = 998)]
		public bool IsDeleted { get; set; } = false;

		[Column("deleted_at", Order = 999)]
		public DateTime? DeletedAt { get; set; }
	}
}
