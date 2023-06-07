
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Models
{
	[Table("users")]
	public class User : BaseEntity
	{
		[Key]
		[Column("id", Order = 0)]
		public int Id { get; set; }

		[Required]
		[Column("first_name", Order = 1)]
		public string FirstName { get; set; }

		[Required]
		[Column("last_name", Order = 2)]
		public string LastName { get; set; }

		[Required]
		[Column("email", Order = 3)]
		public string Email { get; set; }

		[Required]
		[Column("password", Order = 4)]
		public string Password { get; set; }

		[Column("role_id", Order = 5)]
		public int RoleId { get; set; }

		[ForeignKey("RoleId"), DataType("int")]
		public Role Role { get; set; }
	}
}