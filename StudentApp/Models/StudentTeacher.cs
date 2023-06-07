using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Models
{
	[Table("student_teacher")]
	public class StudentTeacher : BaseEntity
	{
		[Key]
		[Column("id")]
		public int Id { get; set; }

		[ForeignKey("student_id"), DataType("int")]
		public User Students { get; set; }

		[ForeignKey("teacher_id"), DataType("int")]
		public User Teachers { get; set; }
	}
}
