using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Models
{
	public class Role
	{
		[Key]
		public int id { get; set; }
		[Required]
		public string name { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreatedAt { get; set; } = DateTime.Now;

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]

		public DateTime UpdatedAt { get; set; } = DateTime.Now;

		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }

		public ICollection<User> Users { get; set; }

	}
}
