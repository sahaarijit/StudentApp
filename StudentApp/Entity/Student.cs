using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Entity
{
	[Table("students")]
	public class Student : BaseEntity
	{
		public int Id { get; set; }

		public int StudentId { get; set; }

		[ForeignKey("StudentId"), DataType("int")]

		public virtual User user { get; set; }

		public ICollection<StudentTeacher> Students { get; set; }

	}
}
