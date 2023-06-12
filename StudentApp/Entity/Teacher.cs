﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentApp.Entity

{
	[Table("teachers")]
	public class Teacher : BaseEntity
	{
		public int Id { get; set; }

		[Column("teacher_id", Order = 1)]
		public int TeacherId { get; set; }

		[ForeignKey("TeacherId"), DataType("int")]
		public virtual User user { get; set; }

		public ICollection<StudentTeacher> Teachers { get; set; }

	}
}
