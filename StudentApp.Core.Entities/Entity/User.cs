using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace StudentApp.Core.Entities
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

		[JsonIgnore]
		[IgnoreDataMember]
		public virtual Student student { get; set; }
		[JsonIgnore]
		[IgnoreDataMember]
		public virtual Teacher teacher { get; set; }
	}
}