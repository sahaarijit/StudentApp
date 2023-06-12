using Microsoft.AspNetCore.Mvc;
using StudentApp.Data;
using StudentApp.Dto;
using StudentApp.Entity;

namespace StudentApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{
		private ApplicationDbContext _context;
		public StudentController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpPost]
		public IActionResult Student(StudentDto studentDto)
		{
			var user = _context.Users.FirstOrDefault(s => s.Id == studentDto.StudentId && s.RoleId == 1);
			if (user == null) {
				return BadRequest("Student is not there with the given id");
			}
			else {
				var student = new Student {
					StudentId = studentDto.StudentId
				};
				_context.Students.Add(student);
				_context.SaveChanges();
				return Ok("student created");
			}
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var student = _context.Students.FirstOrDefault(s => s.StudentId == id);
			if (student == null) {
				return BadRequest("student not found");
			}
			if (student.IsDeleted == false) {
				student.IsDeleted = true;
				student.DeletedAt = DateTime.Now;
				_context.SaveChanges();
				return Ok("student is successfully deleted");
			}
			else {
				student.IsDeleted = false;
				student.DeletedAt = null;
				_context.SaveChanges();
				return Ok("student is successfully updated");
			}
		}


		[HttpPut("{id}")]
		public IActionResult Put(int id, StudentDto studentDto)
		{
			var user = _context.Users.FirstOrDefault(s => s.Id == studentDto.StudentId && s.RoleId == 1);
			if (user == null) {
				return BadRequest("Student is not there with the given id");
			}
			else {
				var student = new Student {
					Id = id,
					StudentId = studentDto.StudentId
				};
				_context.Students.Update(student);
				_context.SaveChanges();
				return Ok("student Updated");
			}
		}

		[HttpGet]
		public IEnumerable<Student> GetStudents()
		{
			return _context.Students.Where(s => s.IsDeleted == false);
		}
	}

}
