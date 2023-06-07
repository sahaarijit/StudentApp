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

		[Column("student_id", Order = 1)]
		public int StudentId { get; set; }

		[Column("teacher_id", Order = 2)]
		public int TeacherId { get; set; }

		[ForeignKey("StudentId"), DataType("int")]
		public User Students { get; set; }


		[ForeignKey("TeacherId"), DataType("int")]
		public User Teachers { get; set; }
	}
}
