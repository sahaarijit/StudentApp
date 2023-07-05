using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace StudentApp.Core.Entities
{
	[Table("roles")]
	public class Role : BaseEntity
	{
		public int Id { get; set; }

		public string Name { get; set; }

		[JsonIgnore]
		[IgnoreDataMember]
		public ICollection<User> users { get; set; }
	}
}
