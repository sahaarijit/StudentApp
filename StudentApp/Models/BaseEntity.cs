
using System.ComponentModel.DataAnnotations.Schema;
namespace StudentApp.Models
{
	public class BaseEntity
	{
		[Column("created_at")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		[Column("updated_at")]
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedAt { get; set; } = DateTime.Now;

		[Column("is_deleted")]
		public bool IsDeleted { get; set; } = false;

		[Column("deleted_at")]
		public DateTime? DeletedAt { get; set; }
	}
}
