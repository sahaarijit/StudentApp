using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Entity

{
	[Table("student_teacher")]
	public class StudentTeacher : BaseEntity
	{

		public int Id { get; set; }

		public int StudentId { get; set; }

		public int TeacherId { get; set; }

		[ForeignKey("StudentId"), DataType("int")]
		public virtual Student Student { get; set; }

		[ForeignKey("TeacherId"), DataType("int")]
		public virtual Teacher Teacher { get; set; }
	}
}
