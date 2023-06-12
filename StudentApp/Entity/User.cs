using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Entity
{
	[Table("users")]
	public class User : BaseEntity
	{
		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }

		public int RoleId { get; set; }

		[ForeignKey("RoleId"), DataType("int")]
		public virtual Role role { get; set; }

		public virtual Student student { get; set; }
		public virtual Teacher teacher { get; set; }
	}
}