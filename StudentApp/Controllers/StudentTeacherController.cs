using Microsoft.AspNetCore.Mvc;
using StudentApp.Data;
using StudentApp.Dto;
using StudentApp.Entity;

namespace StudentApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentTeacherController : ControllerBase
	{
		private ApplicationDbContext _context;
		public StudentTeacherController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public IActionResult StudentTeacher(StudentTeacherDto studentTeacher)
		{

			var studentTeacher1 = new StudentTeacher {
				StudentId = studentTeacher.StudentId,
				TeacherId = studentTeacher.TeacherId
			};
			_context.StudentTeacher.Add(studentTeacher1);
			_context.SaveChanges();
			return Ok("Student and teacher are assigned to each other");
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var studetTeacher = _context.StudentTeacher.FirstOrDefault(s => s.Id == id);
			if (studetTeacher == null) {
				return BadRequest("teacher not found");
			}
			if (studetTeacher.IsDeleted == false) {
				studetTeacher.IsDeleted = true;
				studetTeacher.DeletedAt = DateTime.Now;
				_context.SaveChanges();
				return Ok("StudentTeacher is successfully deleted");
			}
			else {
				studetTeacher.IsDeleted = false;
				studetTeacher.DeletedAt = null;
				_context.SaveChanges();
				return Ok("StudentTeacher is successfully updated");
			}
		}

		[HttpPut("{id}")]
		public IActionResult actionResult(int id, StudentTeacherDto studentTeacherDto)
		{
			var studentTeacher1 = new StudentTeacher {
				Id = id,
				StudentId = studentTeacherDto.StudentId,
				TeacherId = studentTeacherDto.TeacherId
			};
			_context.StudentTeacher.Update(studentTeacher1);
			_context.SaveChanges();
			return Ok("StudentTeacher are updated to each other");
		}

		[HttpGet]
		public IEnumerable<StudentTeacher> GetStudents()
		{
			return _context.StudentTeacher.Where(s => s.IsDeleted == false);
		}
	}
}
