using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Models
{
	[Table("roles")]
	public class Role : BaseEntity
	{
		[Key]
		[Column("id", Order = 0)]
		public int Id { get; set; }

		[Required]
		[Column("name", Order = 1)]
		public string Name { get; set; }
	}
}
