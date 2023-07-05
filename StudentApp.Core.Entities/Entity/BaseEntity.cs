namespace StudentApp.Core.Entities
{
	public class BaseEntity
	{
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		public DateTime UpdatedAt { get; set; } = DateTime.Now;

		public bool IsDeleted { get; set; } = false;

		public DateTime? DeletedAt { get; set; }
	}
}
