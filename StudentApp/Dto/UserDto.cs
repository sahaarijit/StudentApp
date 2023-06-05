using System.ComponentModel.DataAnnotations;

namespace StudentApp.Dto
{
	public class UserDto
	{

		[Required]
		public string email { get; set; }

		[Required]
		public string password { get; set; }

		[Required]
		public int RoleId { get; set; }
	}
}
