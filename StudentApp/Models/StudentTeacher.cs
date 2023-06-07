using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Models
{


	public class StudentTeacher
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("Student")]
		public int StudentId { get; set; }

		[ForeignKey("Teacher")]
		public int TeacherId { get; set; }


		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreatedAt { get; set; } = DateTime.Now;


		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedAt { get; set; } = DateTime.Now;


		public bool IsDeleted { get; set; } = false;
		public DateTime? DeletedAt { get; set; }

		public virtual User Student { get; set; }
		public virtual User Teacher { get; set; }

	}
}
