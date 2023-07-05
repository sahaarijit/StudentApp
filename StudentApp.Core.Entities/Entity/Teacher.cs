using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Core.Entities
{
	[Table("teachers")]
	public class Teacher : BaseEntity
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		[ForeignKey("UserId"), DataType("int")]
		public virtual User user { get; set; }

		public ICollection<StudentTeacher> Teachers { get; set; }
	}
}
