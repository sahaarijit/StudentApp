using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Models
{
	[Table("student_teacher")]
	public class StudentTeacher : BaseEntity
	{
		[Key]
		[Column("id", Order = 0)]
		public int Id { get; set; }

		[ForeignKey("student_id"), DataType("int")]
		[Column(Order = 1)]
		public User Students { get; set; }

		[Column(Order = 2)]
		[ForeignKey("teacher_id"), DataType("int")]
		public User Teachers { get; set; }
	}
}
