
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace StudentApp.Models
{
	//[UserValidator]
	public class User
	{
		[Key]
		public int id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		[ForeignKey("Role")]
		public int RoleId { get; set; }

		[Required]
		public string email { get; set; }

		[Required]
		public string password { get; set; }


		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreatedAt { get; set; } = DateTime.Now;


		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedAt { get; set; } = DateTime.Now;

		public bool IsDeleted { get; set; } = false;

		public DateTime? DeletedAt { get; set; }


		//public Role Role { get; set; }

	}
}
