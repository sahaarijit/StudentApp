
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Models
{
	[Table("users")]
	public class User : BaseEntity
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[Required]
		[Column("first_name")]
		public string FirstName { get; set; }

		[Required]
		[Column("last_name")]
		public string LastName { get; set; }

		[Required]
		[Column("email")]
		public string Email { get; set; }

		[Required]
		[Column("password")]
		public string Password { get; set; }

		[ForeignKey("role_id"), DataType("int")]
		public Role Role { get; set; }
	}
}
