using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Models
{
	[Table("roles")]
	public class Role : BaseEntity
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[Required]
		[Column("name")]
		public string Name { get; set; }
	}
}
